// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;
using Domain.Enums;

namespace Api.BackgroundServices;

/// <summary>
/// Periodically scans for residents whose data has exceeded its retention period
/// and creates AnonymizationCandidate records for Admin review (UC-010).
/// </summary>
/// <remarks>
/// Runs once every 24 hours by default. Does NOT anonymise data directly; always
/// requires Admin approval through the Anonymisation Queue.
///
/// The "inactivity" trigger is computed as the most recent activity timestamp among
/// ResidentNote.EditedAt, MedicineRecord.Timestamp and PainkillerRecord.GivenAt for
/// each resident. If that timestamp is older than the retention period of the
/// RetentionDataCategory.AnonymizationTrigger policy (default 90 days per UC-010
/// "Default Retention Values"), an AnonymizationCandidate(Pending) record is created.
///
/// References:
///   - GDPR Art. 5(1)(e): storage limitation.
///   - Datatilsynet "Trin 3: Husk at slette" – deletion must occur continuously and
///     automatically once the retention purpose has lapsed.
///   - Autorisationsloven §22: MedicineRecord retention is enforced separately at
///     the repository level and is NOT in scope for this scan.
/// </remarks>
public class RetentionBackgroundService : BackgroundService
{
    #region Fields

    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RetentionBackgroundService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromHours(24);

    #endregion

    #region Constructors

    public RetentionBackgroundService(
        IServiceProvider serviceProvider,
        ILogger<RetentionBackgroundService> logger)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(logger);
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    #endregion

    #region Methods

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("RetentionBackgroundService started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ScanForCandidatesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in retention scan loop.");
            }

            await Task.Delay(_interval, stoppingToken);
        }

        _logger.LogInformation("RetentionBackgroundService stopped.");
    }

    /// <summary>
    /// Scans residents against retention policies and creates pending anonymisation candidates.
    /// </summary>
    private async Task ScanForCandidatesAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        IRetentionPolicyRepository policyRepository =
            scope.ServiceProvider.GetRequiredService<IRetentionPolicyRepository>();
        IResidentRepository residentRepository =
            scope.ServiceProvider.GetRequiredService<IResidentRepository>();
        IResidentNoteRepository noteRepository =
            scope.ServiceProvider.GetRequiredService<IResidentNoteRepository>();
        IMedicineRepository medicineRepository =
            scope.ServiceProvider.GetRequiredService<IMedicineRepository>();
        IPainkillerRepository painkillerRepository =
            scope.ServiceProvider.GetRequiredService<IPainkillerRepository>();
        IAnonymizationCandidateRepository candidateRepository =
            scope.ServiceProvider.GetRequiredService<IAnonymizationCandidateRepository>();

        IEnumerable<RetentionPolicy> policies = await policyRepository.GetAllAsync(cancellationToken);
        RetentionPolicy? triggerPolicy = policies.FirstOrDefault(
            p => p.Category == RetentionDataCategory.AnonymizationTrigger);

        if (triggerPolicy is null)
        {
            // Operator has not seeded an AnonymizationTrigger policy yet; log and skip
            // so that the absence is visible in audit but does not crash the service.
            _logger.LogWarning(
                "Retention scan skipped: no RetentionPolicy with Category=AnonymizationTrigger configured.");
            return;
        }

        IEnumerable<Resident> residents = await residentRepository.GetAllAsync(cancellationToken);
        IEnumerable<ResidentNote> notes = await noteRepository.GetAllAsync(cancellationToken);
        IEnumerable<MedicineRecord> medicines = await medicineRepository.GetAllAsync(cancellationToken);
        IEnumerable<PainkillerRecord> painkillers = await painkillerRepository.GetAllAsync(cancellationToken);
        IEnumerable<AnonymizationCandidate> existing =
            await candidateRepository.GetAllAsync(cancellationToken);

        DateTime cutoffUtc = DateTime.UtcNow - triggerPolicy.RetentionPeriod;
        int created = 0;

        foreach (Resident resident in residents)
        {
            // Skip residents already in the anonymisation pipeline to avoid duplicates.
            bool alreadyTracked = existing.Any(c =>
                c.ResidentId == resident.Id &&
                (c.Status == AnonymizationStatus.Pending ||
                 c.Status == AnonymizationStatus.Approved ||
                 c.Status == AnonymizationStatus.Completed ||
                 c.Status == AnonymizationStatus.OnHold));
            if (alreadyTracked)
            {
                continue;
            }

            DateTime? lastActivity = ComputeLastActivityUtc(resident.Id, notes, medicines, painkillers);

            // A resident with no recorded activity at all is treated as "no triggering
            // signal" rather than "inactive forever"; Admin can still create a manual
            // candidate via the UI if a one-off anonymisation is required.
            if (lastActivity is null || lastActivity > cutoffUtc)
            {
                continue;
            }

            AnonymizationCandidate candidate = new()
            {
                Id = Guid.NewGuid(),
                ResidentId = resident.Id,
                RetentionPolicyId = triggerPolicy.Id,
                SuggestedAt = DateTime.UtcNow,
                Reason = $"No recorded activity since {lastActivity:yyyy-MM-dd} "
                         + $"(retention period {triggerPolicy.RetentionPeriod.TotalDays:N0} days exceeded).",
                Status = AnonymizationStatus.Pending
            };
            _ = await candidateRepository.CreateAsync(candidate, cancellationToken);
            created++;
        }

        _logger.LogInformation(
            "Retention scan completed. Loaded {PolicyCount} policies, evaluated {ResidentCount} residents, created {Created} candidates.",
            policies.Count(), residents.Count(), created);
    }

    /// <summary>
    /// Returns the most recent activity timestamp across notes, medicines, and painkillers
    /// for the given resident, or <see langword="null"/> when no activity exists.
    /// </summary>
    private static DateTime? ComputeLastActivityUtc(
        Guid residentId,
        IEnumerable<ResidentNote> notes,
        IEnumerable<MedicineRecord> medicines,
        IEnumerable<PainkillerRecord> painkillers)
    {
        DateTime? lastNote = notes.Where(n => n.ResidentId == residentId)
                                  .Select(n => (DateTime?)n.EditedAt)
                                  .DefaultIfEmpty(null)
                                  .Max();
        DateTime? lastMedicine = medicines.Where(m => m.ResidentId == residentId)
                                          .Select(m => (DateTime?)m.Timestamp)
                                          .DefaultIfEmpty(null)
                                          .Max();
        DateTime? lastPainkiller = painkillers.Where(p => p.ResidentId == residentId)
                                              .Select(p => (DateTime?)p.GivenAt)
                                              .DefaultIfEmpty(null)
                                              .Max();

        DateTime?[] candidates = [lastNote, lastMedicine, lastPainkiller];
        return candidates.Where(d => d.HasValue).DefaultIfEmpty(null).Max();
    }

    #endregion
}
