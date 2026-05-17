// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Enums;

namespace Core.DTOs.Security;

/// <summary>
/// Data transfer object representing a detected security incident (UC-010).
/// </summary>
public class SecurityIncidentDto
{
    public Guid Id { get; set; }
    public DateTime DetectedAt { get; set; }
    public string Type { get; set; } = string.Empty;
    public IncidentSeverity Severity { get; set; }
    public IncidentStatus Status { get; set; }
    public string InvestigationNotes { get; set; } = string.Empty;

    /// <summary>
    /// UTC deadline by which the controller must notify Datatilsynet under GDPR
    /// Art. 33(1): "without undue delay and, where feasible, not later than 72
    /// hours after having become aware of it". For an incident this is
    /// <see cref="DetectedAt"/> + 72 hours; the UI surfaces a warning when the
    /// remaining time drops below 24 hours.
    /// </summary>
    public DateTime BreachNotificationDeadlineUtc { get; set; }
}
