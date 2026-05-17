// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;
using Domain.Enums;

namespace Api.BackgroundServices;

/// <summary>
/// Periodically scans audit logs for suspicious patterns (failed admin operations,
/// mass administrative actions) and creates SecurityIncident records for Admin review (UC-010).
/// </summary>
/// <remarks>
/// Runs every 15 minutes by default. Detected incidents enter the SecurityIncident
/// queue with Status=Open and require manual Admin investigation. Escalation as a
/// personal data breach triggers GDPR Art. 33 notification to the DPO (handled by
/// SecurityIncidentService.EscalateIncidentAsync).
///
/// Detection scope and limitations:
///   - The AuditInterceptor (UC-009) only records EF Core SaveChanges. Login attempts
///     that do not write to the database (i.e. password mismatches that short-circuit
///     before any persistence) are NOT captured here. A dedicated authentication log
///     is required to cover the full "fejlede loginforsøg" scenario flagged by
///     Datatilsynet's "Logning af brugernes anvendelser af personoplysninger" guidance.
///     Until that log exists this service evaluates failed-SaveChanges patterns only.
///   - Off-hours administrative access detection requires HTTP request logging at
///     the middleware layer and is therefore deferred to a future iteration.
///
/// Thresholds applied (risk-based, EDPB case-study aligned):
///   - 10 or more failed audit entries by the same user within 60 minutes
///     => SecurityIncident with Severity=Medium, Type="RepeatedFailures".
/// </remarks>
public class IncidentDetectionService : BackgroundService
{
    #region Fields

    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<IncidentDetectionService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(15);
    private readonly TimeSpan _failureWindow = TimeSpan.FromMinutes(60);
    private const int FailureThreshold = 10;

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
    /// Evaluates audit entries against detection rules and creates SecurityIncident records.
    /// </summary>
    private async Task DetectIncidentsAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        IAuditRepository auditRepository =
            scope.ServiceProvider.GetRequiredService<IAuditRepository>();
        ISecurityIncidentRepository incidentRepository =
            scope.ServiceProvider.GetRequiredService<ISecurityIncidentRepository>();

        DateTime windowStart = DateTime.UtcNow - _failureWindow;
        IEnumerable<AuditEntry> recent = await auditRepository.GetAllAsync(cancellationToken);
        List<AuditEntry> failuresInWindow = recent
            .Where(a => !a.Succeeded && a.StartTimeUtc >= windowStart)
            .ToList();

        var byUser = failuresInWindow
            .GroupBy(a => a.UserId)
            .Where(g => g.Count() >= FailureThreshold)
            .ToList();

        List<SecurityIncident> existingOpen =
            (await incidentRepository.GetAllAsync(cancellationToken))
                .Where(i => i.Status == IncidentStatus.Open ||
                            i.Status == IncidentStatus.UnderInvestigation)
                .ToList();

        int created = 0;
        foreach (var group in byUser)
        {
            // Idempotency: only one open RepeatedFailures incident per user at a time.
            bool alreadyOpen = existingOpen.Any(i =>
                i.Type == "RepeatedFailures" && i.ReportedByEmployeeId == group.Key);
            if (alreadyOpen)
            {
                continue;
            }

            SecurityIncident incident = new()
            {
                Id = Guid.NewGuid(),
                DetectedAt = DateTime.UtcNow,
                Type = "RepeatedFailures",
                Severity = IncidentSeverity.Medium,
                Status = IncidentStatus.Open,
                InvestigationNotes =
                    $"Detected {group.Count()} failed audit entries within {_failureWindow.TotalMinutes:N0} minutes "
                    + $"for user {group.Key}. Review the audit log around {group.Min(g => g.StartTimeUtc):yyyy-MM-dd HH:mm} UTC.",
                ReportedByEmployeeId = group.Key
            };
            _ = await incidentRepository.CreateAsync(incident, cancellationToken);
            created++;
        }

        _logger.LogInformation(
            "Incident detection scan completed at {Timestamp}. Failures in window: {FailureCount}, incidents created: {Created}.",
            DateTime.UtcNow, failuresInWindow.Count, created);
    }

    #endregion
}
