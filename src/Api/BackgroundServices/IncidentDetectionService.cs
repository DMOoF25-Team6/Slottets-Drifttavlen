// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;
using Domain.Enums;

using Microsoft.AspNetCore.Identity;

namespace Api.BackgroundServices;

/// <summary>
/// Periodically scans audit and authentication logs for patterns that may
/// indicate a personal data breach and creates <see cref="SecurityIncident"/>
/// records for Admin review (UC-010).
/// </summary>
/// <remarks>
/// Runs every 15 minutes. Detection rules are deliberately conservative so the
/// queue is signal-rich; tuning thresholds is operator policy and is documented
/// in the constants below.
///
/// Rules (each scoped to the last <see cref="_lookbackWindow"/>):
///   <list type="number">
///     <item><b>RepeatedFailures</b>: 10+ failed <see cref="AuditEntry"/> rows by
///       the same user => Medium severity. Catches sustained app-level errors
///       that might mask data exfiltration.</item>
///     <item><b>FailedLoginBruteForce</b>: 5+ failed <see cref="LoginAttempt"/>
///       rows from the same email-hash or IP within 10 minutes => High. Aligns
///       with Datatilsynet's incident catalogue for credential-stuffing attacks.</item>
///     <item><b>OffHoursAdminAccess</b>: a successful login by an account with
///       the <c>admin</c> role between 22:00-06:00 (Europe/Copenhagen) or on a
///       weekend => Low. Visibility only; legitimate after-hours work is common
///       and produces an Open incident the Admin can close immediately.</item>
///     <item><b>MassExportPattern</b>: 5+ <see cref="SubjectAccessRequest"/> rows
///       created by the same employee in 24 hours => Medium. The Art. 15 export
///       endpoint emits a full personal-data bundle, so abnormally many calls
///       merit review.</item>
///   </list>
///
/// References:
///   - Datatilsynet "Logning af brugernes anvendelser af personoplysninger"
///     and "Håndtering af brud på persondatasikkerheden" (May 2025).
///   - GDPR Art. 32(1)(b) integrity and confidentiality controls.
///   - GDPR Art. 33(1) — Art. 33 notification timer starts when the controller
///     becomes aware of the breach; for an Open incident the
///     <c>DetectedAt</c> timestamp is that moment of awareness.
/// </remarks>
public class IncidentDetectionService : BackgroundService
{
    #region Constants

    private const int RepeatedAuditFailureThreshold = 10;
    private const int FailedLoginBruteForceThreshold = 5;
    private const int MassExportThreshold = 5;

    private const string TypeRepeatedFailures = "RepeatedFailures";
    private const string TypeFailedLoginBruteForce = "FailedLoginBruteForce";
    private const string TypeOffHoursAdminAccess = "OffHoursAdminAccess";
    private const string TypeMassExportPattern = "MassExportPattern";

    private const string AdminRoleName = "admin";

    /// <summary>Local-time hour at which administrative activity is considered "off-hours" (inclusive).</summary>
    private const int OffHoursStartHour = 22;
    /// <summary>Local-time hour at which "off-hours" ends (exclusive).</summary>
    private const int OffHoursEndHour = 6;

    /// <summary>Time zone used to evaluate "off-hours" — IANA "Europe/Copenhagen".</summary>
    private static readonly TimeZoneInfo OperatorTimeZone =
        TimeZoneInfo.FindSystemTimeZoneById("Europe/Copenhagen");

    #endregion

    #region Fields

    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<IncidentDetectionService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(15);
    private readonly TimeSpan _lookbackWindow = TimeSpan.FromMinutes(60);
    private readonly TimeSpan _bruteForceWindow = TimeSpan.FromMinutes(10);
    private readonly TimeSpan _massExportWindow = TimeSpan.FromHours(24);

    #endregion

    #region Constructors

    public IncidentDetectionService(
        IServiceProvider serviceProvider,
        ILogger<IncidentDetectionService> logger)
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
        _logger.LogInformation("IncidentDetectionService started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await DetectIncidentsAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in incident detection loop.");
            }

            await Task.Delay(_interval, stoppingToken);
        }

        _logger.LogInformation("IncidentDetectionService stopped.");
    }

    /// <summary>
    /// Runs all detection rules against the most recent data window and persists
    /// any matched incidents. Rules are independent: a failure in one does not
    /// short-circuit the others.
    /// </summary>
    private async Task DetectIncidentsAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        IAuditRepository auditRepository =
            scope.ServiceProvider.GetRequiredService<IAuditRepository>();
        ISecurityIncidentRepository incidentRepository =
            scope.ServiceProvider.GetRequiredService<ISecurityIncidentRepository>();
        ILoginAttemptRepository loginAttemptRepository =
            scope.ServiceProvider.GetRequiredService<ILoginAttemptRepository>();
        ISubjectAccessRequestRepository sarRepository =
            scope.ServiceProvider.GetRequiredService<ISubjectAccessRequestRepository>();
        UserManager<User> userManager =
            scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        List<SecurityIncident> existingOpen =
            (await incidentRepository.GetAllAsync(cancellationToken))
                .Where(i => i.Status == IncidentStatus.Open ||
                            i.Status == IncidentStatus.UnderInvestigation)
                .ToList();

        int created = 0;
        created += await DetectRepeatedAuditFailuresAsync(
            auditRepository, incidentRepository, existingOpen, cancellationToken);
        created += await DetectFailedLoginBruteForceAsync(
            loginAttemptRepository, incidentRepository, existingOpen, cancellationToken);
        created += await DetectOffHoursAdminAccessAsync(
            loginAttemptRepository, userManager, incidentRepository, existingOpen, cancellationToken);
        created += await DetectMassExportPatternAsync(
            sarRepository, incidentRepository, existingOpen, cancellationToken);

        _logger.LogInformation(
            "Incident detection scan completed at {Timestamp}. Incidents created: {Created}.",
            DateTime.UtcNow, created);
    }

    private async Task<int> DetectRepeatedAuditFailuresAsync(
        IAuditRepository auditRepository,
        ISecurityIncidentRepository incidentRepository,
        IReadOnlyCollection<SecurityIncident> existingOpen,
        CancellationToken cancellationToken)
    {
        DateTime windowStart = DateTime.UtcNow - _lookbackWindow;
        List<AuditEntry> failuresInWindow =
            (await auditRepository.GetAllAsync(cancellationToken))
                .Where(a => !a.Succeeded && a.StartTimeUtc >= windowStart)
                .ToList();

        int created = 0;
        var byUser = failuresInWindow.GroupBy(a => a.UserId)
            .Where(g => g.Count() >= RepeatedAuditFailureThreshold);
        foreach (var group in byUser)
        {
            bool alreadyOpen = existingOpen.Any(i =>
                i.Type == TypeRepeatedFailures && i.ReportedByEmployeeId == group.Key);
            if (alreadyOpen)
            {
                continue;
            }

            SecurityIncident incident = new()
            {
                Id = Guid.NewGuid(),
                DetectedAt = DateTime.UtcNow,
                Type = TypeRepeatedFailures,
                Severity = IncidentSeverity.Medium,
                Status = IncidentStatus.Open,
                InvestigationNotes =
                    $"Detected {group.Count()} failed audit entries within {_lookbackWindow.TotalMinutes:N0} minutes "
                    + $"for user {group.Key}. Earliest at {group.Min(g => g.StartTimeUtc):yyyy-MM-dd HH:mm} UTC.",
                ReportedByEmployeeId = group.Key
            };
            _ = await incidentRepository.CreateAsync(incident, cancellationToken);
            created++;
        }
        return created;
    }

    private async Task<int> DetectFailedLoginBruteForceAsync(
        ILoginAttemptRepository loginAttemptRepository,
        ISecurityIncidentRepository incidentRepository,
        IReadOnlyCollection<SecurityIncident> existingOpen,
        CancellationToken cancellationToken)
    {
        DateTime windowStart = DateTime.UtcNow - _bruteForceWindow;
        List<LoginAttempt> failuresInWindow =
            (await loginAttemptRepository.GetAllAsync(cancellationToken))
                .Where(a => !a.Succeeded && a.AttemptedAt >= windowStart)
                .ToList();

        int created = 0;

        // Group by email hash first (catches credential-stuffing against one account)
        foreach (var group in failuresInWindow.GroupBy(a => a.EmailHash)
                                              .Where(g => g.Count() >= FailedLoginBruteForceThreshold))
        {
            string fingerprint = $"email:{group.Key}";
            if (existingOpen.Any(i => i.Type == TypeFailedLoginBruteForce && i.InvestigationNotes.Contains(fingerprint)))
            {
                continue;
            }

            SecurityIncident incident = new()
            {
                Id = Guid.NewGuid(),
                DetectedAt = DateTime.UtcNow,
                Type = TypeFailedLoginBruteForce,
                Severity = IncidentSeverity.High,
                Status = IncidentStatus.Open,
                InvestigationNotes =
                    $"{group.Count()} failed logins within {_bruteForceWindow.TotalMinutes:N0} minutes "
                    + $"against email hash. Fingerprint: {fingerprint}. "
                    + $"Earliest at {group.Min(g => g.AttemptedAt):yyyy-MM-dd HH:mm} UTC."
            };
            _ = await incidentRepository.CreateAsync(incident, cancellationToken);
            created++;
        }

        // Then by source IP (catches spraying across many accounts from one host)
        foreach (var group in failuresInWindow.GroupBy(a => a.IpAddress)
                                              .Where(g => g.Count() >= FailedLoginBruteForceThreshold))
        {
            string fingerprint = $"ip:{group.Key}";
            if (existingOpen.Any(i => i.Type == TypeFailedLoginBruteForce && i.InvestigationNotes.Contains(fingerprint)))
            {
                continue;
            }

            SecurityIncident incident = new()
            {
                Id = Guid.NewGuid(),
                DetectedAt = DateTime.UtcNow,
                Type = TypeFailedLoginBruteForce,
                Severity = IncidentSeverity.High,
                Status = IncidentStatus.Open,
                InvestigationNotes =
                    $"{group.Count()} failed logins within {_bruteForceWindow.TotalMinutes:N0} minutes "
                    + $"from a single source. Fingerprint: {fingerprint}. "
                    + $"Earliest at {group.Min(g => g.AttemptedAt):yyyy-MM-dd HH:mm} UTC."
            };
            _ = await incidentRepository.CreateAsync(incident, cancellationToken);
            created++;
        }

        return created;
    }

    private async Task<int> DetectOffHoursAdminAccessAsync(
        ILoginAttemptRepository loginAttemptRepository,
        UserManager<User> userManager,
        ISecurityIncidentRepository incidentRepository,
        IReadOnlyCollection<SecurityIncident> existingOpen,
        CancellationToken cancellationToken)
    {
        DateTime windowStart = DateTime.UtcNow - _lookbackWindow;
        List<LoginAttempt> recentSuccesses =
            (await loginAttemptRepository.GetAllAsync(cancellationToken))
                .Where(a => a.Succeeded && a.UserId.HasValue && a.AttemptedAt >= windowStart)
                .ToList();

        int created = 0;
        foreach (LoginAttempt attempt in recentSuccesses)
        {
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(attempt.AttemptedAt, OperatorTimeZone);
            if (!IsOffHours(localTime))
            {
                continue;
            }

            User? user = await userManager.FindByIdAsync(attempt.UserId!.Value.ToString());
            if (user is null || !await userManager.IsInRoleAsync(user, AdminRoleName))
            {
                continue;
            }

            // Idempotent per user per local calendar day: one incident is enough
            // signal for the Admin to inspect the whole off-hours window for that user.
            string fingerprint = $"user:{user.Id}|day:{localTime:yyyy-MM-dd}";
            if (existingOpen.Any(i => i.Type == TypeOffHoursAdminAccess && i.InvestigationNotes.Contains(fingerprint)))
            {
                continue;
            }

            SecurityIncident incident = new()
            {
                Id = Guid.NewGuid(),
                DetectedAt = DateTime.UtcNow,
                Type = TypeOffHoursAdminAccess,
                Severity = IncidentSeverity.Low,
                Status = IncidentStatus.Open,
                InvestigationNotes =
                    $"Admin login at {localTime:yyyy-MM-dd HH:mm} local "
                    + $"({attempt.AttemptedAt:yyyy-MM-dd HH:mm 'UTC'}). "
                    + $"Off-hours window: {OffHoursStartHour:00}:00-{OffHoursEndHour:00}:00 weekdays + weekends. "
                    + $"Fingerprint: {fingerprint}.",
                ReportedByEmployeeId = user.Id
            };
            _ = await incidentRepository.CreateAsync(incident, cancellationToken);
            created++;
        }
        return created;
    }

    private async Task<int> DetectMassExportPatternAsync(
        ISubjectAccessRequestRepository sarRepository,
        ISecurityIncidentRepository incidentRepository,
        IReadOnlyCollection<SecurityIncident> existingOpen,
        CancellationToken cancellationToken)
    {
        DateTime windowStart = DateTime.UtcNow - _massExportWindow;
        List<SubjectAccessRequest> recent =
            (await sarRepository.GetAllAsync(cancellationToken))
                .Where(s => s.RequestedAt >= windowStart)
                .ToList();

        int created = 0;
        foreach (var group in recent.GroupBy(s => s.RequestedByEmployeeId)
                                    .Where(g => g.Count() >= MassExportThreshold))
        {
            bool alreadyOpen = existingOpen.Any(i =>
                i.Type == TypeMassExportPattern && i.ReportedByEmployeeId == group.Key);
            if (alreadyOpen)
            {
                continue;
            }

            SecurityIncident incident = new()
            {
                Id = Guid.NewGuid(),
                DetectedAt = DateTime.UtcNow,
                Type = TypeMassExportPattern,
                Severity = IncidentSeverity.Medium,
                Status = IncidentStatus.Open,
                InvestigationNotes =
                    $"Employee {group.Key} created {group.Count()} subject access exports "
                    + $"in the last {_massExportWindow.TotalHours:N0} hours. "
                    + $"Earliest at {group.Min(g => g.RequestedAt):yyyy-MM-dd HH:mm} UTC.",
                ReportedByEmployeeId = group.Key
            };
            _ = await incidentRepository.CreateAsync(incident, cancellationToken);
            created++;
        }
        return created;
    }

    /// <summary>
    /// Returns true when the supplied local time falls inside the operator's
    /// off-hours window: weekdays 22:00-06:00 or the full Saturday/Sunday range.
    /// </summary>
    private static bool IsOffHours(DateTime localTime)
    {
        if (localTime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            return true;
        }
        int hour = localTime.Hour;
        return hour >= OffHoursStartHour || hour < OffHoursEndHour;
    }

    #endregion
}
