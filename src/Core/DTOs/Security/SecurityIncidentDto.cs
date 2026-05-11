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
}
