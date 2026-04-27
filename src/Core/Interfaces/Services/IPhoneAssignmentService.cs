// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;

namespace Core.Interfaces.Services;

/// <summary>
/// Defines a contract for managing phone assignments on the dashboard.
/// </summary>
/// <remarks>
/// Provides methods for retrieving phone assignments for the active shift.
/// Implemented by <see cref="Core.Services.PhoneAssignmentService"/>.
/// Core defines the contract; Infrastructure fulfils it via <see cref="Core.Interfaces.Managers.IPhoneAssignmentManager"/>.
/// </remarks>
public interface IPhoneAssignmentService
{
    /// <summary>
    /// Gets the current phone assignments for the active shift.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of <see cref="PhoneAssignmentDto"/> for the active shift.</returns>
    Task<IEnumerable<PhoneAssignmentDto>> GetCurrentPhoneAssignmentsForActiveShiftAsync(CancellationToken cancellationToken);
}
