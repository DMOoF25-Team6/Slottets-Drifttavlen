// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Security;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;

namespace Core.Services;

/// <summary>
/// Service for managing security incidents (UC-010).
/// </summary>
/// <remarks>
/// Escalation with isBreach=true triggers GDPR Art. 33 notification flow via <see cref="IArt33NotificationService"/>.
/// </remarks>
public class SecurityIncidentService : ISecurityIncidentService
{
    private readonly ISecurityIncidentManager _securityIncidentManager;
    private readonly IArt33NotificationService _art33NotificationService;

    public SecurityIncidentService(
        ISecurityIncidentManager securityIncidentManager,
        IArt33NotificationService art33NotificationService)
    {
        ArgumentNullException.ThrowIfNull(securityIncidentManager);
        ArgumentNullException.ThrowIfNull(art33NotificationService);
        _securityIncidentManager = securityIncidentManager;
        _art33NotificationService = art33NotificationService;
    }

    public Task<IEnumerable<SecurityIncidentDto>> GetIncidentsAsync(CancellationToken cancellationToken)
        => _securityIncidentManager.GetIncidentsAsync(cancellationToken);

    public Task<SecurityIncidentDto> EscalateIncidentAsync(Guid incidentId, bool isBreach, CancellationToken cancellationToken)
        => _securityIncidentManager.EscalateIncidentAsync(incidentId, isBreach, cancellationToken);

    public Task<SecurityIncidentDto> AddInvestigationNotesAsync(AddInvestigationNotesDto dto, CancellationToken cancellationToken)
        => _securityIncidentManager.AddInvestigationNotesAsync(dto, cancellationToken);

    public Task<SecurityIncidentDto> CloseIncidentAsync(Guid incidentId, CancellationToken cancellationToken)
        => _securityIncidentManager.CloseIncidentAsync(incidentId, cancellationToken);
}
