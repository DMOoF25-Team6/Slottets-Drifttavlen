// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Anonymization;

using Domain.Entities;

namespace Core.Mappers;

/// <summary>
/// Static mapper between <see cref="AnonymizationCandidate"/> entities and DTOs (UC-010).
/// </summary>
public static class AnonymizationCandidateMapper
{
    public static AnonymizationCandidateDto ToDto(AnonymizationCandidate entity) => new()
    {
        Id = entity.Id,
        ResidentId = entity.ResidentId,
        RetentionPolicyId = entity.RetentionPolicyId,
        SuggestedAt = entity.SuggestedAt,
        Reason = entity.Reason,
        Status = entity.Status
    };
}
