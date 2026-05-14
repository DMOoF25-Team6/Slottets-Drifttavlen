// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Services;

using Microsoft.Extensions.Logging;

namespace Core.Services;

/// <summary>
/// Stub implementation of GDPR Article 33 breach notification to the DPO (UC-010).
/// </summary>
/// <remarks>
/// Logs the notification event to ILogger. Audit trail of the corresponding
/// SecurityIncident state change (Status → BreachNotified) is captured automatically
/// by the AuditInterceptor when the SecurityIncident entity is updated via SaveChanges.
/// 
/// Production implementation should send actual email via SMTP (MailKit) — to be added in a follow-up branch.
/// </remarks>
public class Art33NotificationService : IArt33NotificationService
{
    #region Fields

    private readonly ILogger<Art33NotificationService> _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Art33NotificationService"/> class.
    /// </summary>
    /// <param name="logger">Logger for breach notification events.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="logger"/> parameter is <see langword="null"/>.</exception>
    public Art33NotificationService(ILogger<Art33NotificationService> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Sends an Art. 33 breach notification to the Data Protection Officer.
    /// </summary>
    /// <param name="incidentId">The unique identifier of the security incident being notified.</param>
    /// <param name="dpoEmail">The DPO's email address.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the notification was logged successfully; otherwise, <see langword="false"/>.</returns>
    /// <remarks>
    /// Current stub logs to ILogger only. Production SMTP implementation pending.
    /// </remarks>
    public Task<bool> SendNotificationAsync(Guid incidentId, string dpoEmail, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(dpoEmail))
        {
            _logger.LogError(
                "Cannot send Art. 33 notification for incident {IncidentId}: DPO email is missing.",
                incidentId);
            return Task.FromResult(false);
        }

        // STUB: production should send via SMTP (e.g., MailKit).
        // The SecurityIncident status change to BreachNotified is automatically
        // captured by AuditInterceptor for Datatilsynet traceability (UC-009).
        _logger.LogWarning(
            "GDPR Art. 33 BREACH NOTIFICATION (STUB) — Incident: {IncidentId}, DPO: {DpoEmail}, Timestamp: {Timestamp}",
            incidentId, dpoEmail, DateTime.UtcNow);

        return Task.FromResult(true);
    }

    #endregion
}
