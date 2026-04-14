// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Managers;

namespace Core.Services;

/// <summary>
/// Service for managing phone assignments on the dashboard.
/// </summary>
public class PhoneAssignmentService(IPhoneAssignmentManager phoneAssignmentManager)
{
    private readonly IPhoneAssignmentManager _phoneAssignmentManager = phoneAssignmentManager;

    /// <summary>
    /// Gets the current phone assignments for the active shift.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A collection of phone assignments for the active shift.</returns>
    public async Task<IEnumerable<PhoneAssignmentDto>> GetCurrentPhoneAssignmentsForActiveShift(CancellationToken cancellationToken)
    {
        return await _phoneAssignmentManager.GetCurrentPhoneAssignmentsForActiveShift(cancellationToken);
    }
}
