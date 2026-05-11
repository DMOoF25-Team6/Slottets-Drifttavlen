// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Security;

/// <summary>
/// Request payload for appending investigation notes to a security incident (UC-010).
/// </summary>
public class AddInvestigationNotesDto
{
    public Guid IncidentId { get; set; }
    public string Notes { get; set; } = string.Empty;
}
