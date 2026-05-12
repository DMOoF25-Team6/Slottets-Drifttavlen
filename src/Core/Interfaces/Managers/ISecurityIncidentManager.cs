// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Security;

namespace Core.Interfaces.Managers;

/// <summary>
/// Defines a contract for managing security incidents (UC-010).
/// </summary>
public interface ISecurityIncidentManager
{
    Task<IEnumerable<SecurityIncidentDto>> GetIncidentsAsync(CancellationToken cancellationToken);
    Task<SecurityIncidentDto> EscalateIncidentAsync(Guid incidentId, bool isBreach, CancellationToken cancellationToken);
    Task<SecurityIncidentDto> AddInvestigationNotesAsync(AddInvestigationNotesDto dto, CancellationToken cancellationToken);
    Task<SecurityIncidentDto> CloseIncidentAsync(Guid incidentId, CancellationToken cancellationToken);
}
