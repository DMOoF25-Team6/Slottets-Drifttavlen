// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Anonymization;

namespace Core.Interfaces.Services;

/// <summary>
/// Defines a contract for the anonymization service (UC-010).
/// </summary>
public interface IAnonymizationService
{
    Task<IEnumerable<AnonymizationCandidateDto>> GetCandidatesAsync(CancellationToken cancellationToken);
    Task<AnonymizationResultDto> ApproveAnonymizationAsync(Guid candidateId, CancellationToken cancellationToken);
    Task<bool> RejectAnonymizationAsync(Guid candidateId, string reason, CancellationToken cancellationToken);
}
