// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Retention;

using Domain.Entities;

namespace Core.Mappers;

/// <summary>
/// Static mapper between <see cref="RetentionPolicy"/> entities and DTOs (UC-010).
/// </summary>
public static class RetentionPolicyMapper
{
    public static RetentionPolicyDto ToDto(RetentionPolicy entity) => new()
    {
        Id = entity.Id,
        Category = entity.Category,
        RetentionPeriod = entity.RetentionPeriod,
        LegalMinimum = entity.LegalMinimum,
        EffectiveFrom = entity.EffectiveFrom
    };

    public static RetentionPolicyAuditDto ToAuditDto(RetentionPolicyAudit entity) => new()
    {
        Id = entity.Id,
        RetentionPolicyId = entity.RetentionPolicyId,
        ChangedByEmployeeId = entity.ChangedByEmployeeId,
        PreviousPeriod = entity.PreviousPeriod,
        NewPeriod = entity.NewPeriod,
        ChangedAt = entity.ChangedAt,
        Reason = entity.Reason
    };
}
