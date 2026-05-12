// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;

using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// Represents a detected security incident that may indicate a personal data breach (UC-010).
/// </summary>
/// <remarks>
/// Created by IncidentDetectionService when suspicious patterns match (e.g., failed logins,
/// mass exports, off-hours access). Admin reviews and may escalate to GDPR Art. 33 breach
/// notification to the Data Protection Officer.
/// </remarks>
public class SecurityIncident : IEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTime DetectedAt { get; set; }

    [Required]
    [MaxLength(200)]
    public string Type { get; set; } = string.Empty;

    [Required]
    public IncidentSeverity Severity { get; set; }

    [Required]
    public IncidentStatus Status { get; set; } = IncidentStatus.Open;

    [MaxLength(2000)]
    public string InvestigationNotes { get; set; } = string.Empty;

    public Guid? ReportedByEmployeeId { get; set; }

    public Guid? ResolvedByEmployeeId { get; set; }
}
