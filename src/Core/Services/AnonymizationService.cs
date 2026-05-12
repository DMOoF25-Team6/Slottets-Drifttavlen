// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Anonymization;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;

namespace Core.Services;

/// <summary>
/// Service for managing GDPR anonymization candidates (UC-010).
/// </summary>
/// <remarks>
/// MedicineLogs are pseudonymized rather than fully anonymized per Autorisationsloven §22.
/// </remarks>
public class AnonymizationService : IAnonymizationService
{
    private readonly IAnonymizationManager _anonymizationManager;

    public AnonymizationService(IAnonymizationManager anonymizationManager)
    {
        ArgumentNullException.ThrowIfNull(anonymizationManager);
        _anonymizationManager = anonymizationManager;
    }

    public Task<IEnumerable<AnonymizationCandidateDto>> GetCandidatesAsync(CancellationToken cancellationToken)
        => _anonymizationManager.GetCandidatesAsync(cancellationToken);

    public Task<AnonymizationResultDto> ApproveAnonymizationAsync(Guid candidateId, CancellationToken cancellationToken)
        => _anonymizationManager.ApproveAnonymizationAsync(candidateId, cancellationToken);

    public Task<bool> RejectAnonymizationAsync(Guid candidateId, string reason, CancellationToken cancellationToken)
        => _anonymizationManager.RejectAnonymizationAsync(candidateId, reason, cancellationToken);
}
