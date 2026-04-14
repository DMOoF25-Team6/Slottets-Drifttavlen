// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

namespace Core.Interfaces.Managers;

/// <summary>
/// Provides business logic operations for managing phone assignments on the dashboard.
/// </summary>
public interface IPhoneAssignmentManager
{
    /// <summary>
    /// Gets the current phone assignments for the active shift.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A collection of phone assignments for the active shift.</returns>
    Task<IEnumerable<PhoneAssignmentDto>> GetCurrentPhoneAssignmentsForActiveShift(CancellationToken cancellationToken);
}
