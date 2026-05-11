// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Security;

namespace Core.Interfaces.Services;

/// <summary>
/// Defines a contract for the security incident service (UC-010).
/// </summary>
public interface ISecurityIncidentService
{
    Task<IEnumerable<SecurityIncidentDto>> GetIncidentsAsync(CancellationToken cancellationToken);
    Task<SecurityIncidentDto> EscalateIncidentAsync(Guid incidentId, bool isBreach, CancellationToken cancellationToken);
    Task<SecurityIncidentDto> AddInvestigationNotesAsync(AddInvestigationNotesDto dto, CancellationToken cancellationToken);
    Task<SecurityIncidentDto> CloseIncidentAsync(Guid incidentId, CancellationToken cancellationToken);
}
