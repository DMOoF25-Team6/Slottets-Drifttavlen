// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Retention;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;

namespace Core.Services;

/// <summary>
/// Service for managing GDPR retention policies (UC-010).
/// </summary>
/// <remarks>
/// Delegates all operations to the injected <see cref="IRetentionPolicyManager"/>.
/// Manager handles legal-minimum validation (e.g., Autorisationsloven §22 for MedicineLogs).
/// </remarks>
public class RetentionPolicyService : IRetentionPolicyService
{
    private readonly IRetentionPolicyManager _retentionPolicyManager;

    public RetentionPolicyService(IRetentionPolicyManager retentionPolicyManager)
    {
        ArgumentNullException.ThrowIfNull(retentionPolicyManager);
        _retentionPolicyManager = retentionPolicyManager;
    }

    public Task<IEnumerable<RetentionPolicyDto>> GetPoliciesAsync(CancellationToken cancellationToken)
        => _retentionPolicyManager.GetPoliciesAsync(cancellationToken);

    public Task<RetentionPolicyDto> UpdateRetentionPolicyAsync(
        UpdateRetentionPolicyDto dto,
        Guid changedByEmployeeId,
        CancellationToken cancellationToken)
        => _retentionPolicyManager.UpdateRetentionPolicyAsync(dto, changedByEmployeeId, cancellationToken);
}
