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
    public static SecurityIncidentDto ToDto(SecurityIncident entity) => new()
    {
        Id = entity.Id,
        DetectedAt = entity.DetectedAt,
        Type = entity.Type,
        Severity = entity.Severity,
        Status = entity.Status,
        InvestigationNotes = entity.InvestigationNotes
    };
}
