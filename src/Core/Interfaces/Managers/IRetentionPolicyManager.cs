// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Retention;

namespace Core.Interfaces.Managers;

/// <summary>
/// Defines a contract for managing retention policies (UC-010).
/// </summary>
public interface IRetentionPolicyManager
{
    Task<IEnumerable<RetentionPolicyDto>> GetPoliciesAsync(CancellationToken cancellationToken);
    Task<RetentionPolicyDto> UpdateRetentionPolicyAsync(UpdateRetentionPolicyDto dto, Guid changedByEmployeeId, CancellationToken cancellationToken);
}
