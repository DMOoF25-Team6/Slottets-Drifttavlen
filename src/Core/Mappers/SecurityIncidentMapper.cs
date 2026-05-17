// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Security;

using Domain.Entities;

namespace Core.Mappers;

/// <summary>
/// Static mapper between <see cref="SecurityIncident"/> entities and DTOs (UC-010).
/// </summary>
public static class SecurityIncidentMapper
{
    /// <summary>
    /// GDPR Art. 33(1) requires notification of a personal data breach without
    /// undue delay and at the latest 72 hours after the controller becomes aware
    /// of it. We treat <c>DetectedAt</c> as the awareness instant.
    /// </summary>
    private static readonly TimeSpan Art33NotificationWindow = TimeSpan.FromHours(72);

    public static SecurityIncidentDto ToDto(SecurityIncident entity) => new()
    {
        Id = entity.Id,
        DetectedAt = entity.DetectedAt,
        Type = entity.Type,
        Severity = entity.Severity,
        Status = entity.Status,
        InvestigationNotes = entity.InvestigationNotes,
        BreachNotificationDeadlineUtc = entity.DetectedAt + Art33NotificationWindow
    };
}
