// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Retention;

namespace Core.Interfaces.Services;

/// <summary>
/// Defines a contract for the retention policy service (UC-010).
/// </summary>
public interface IRetentionPolicyService
{
    Task<IEnumerable<RetentionPolicyDto>> GetPoliciesAsync(CancellationToken cancellationToken);
    Task<RetentionPolicyDto> UpdateRetentionPolicyAsync(UpdateRetentionPolicyDto dto, Guid changedByEmployeeId, CancellationToken cancellationToken);
}
