// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;

namespace Core.Services;

/// <summary>
/// Provides business logic operations for managing phone assignments on the dashboard.
/// </summary>
/// <remarks>
/// Delegates all data access to <see cref="IPhoneAssignmentManager"/> in Infrastructure,
/// ensuring Core never depends on Infrastructure or data access concerns directly —
/// following Clean Architecture and the Dependency Inversion Principle.
/// </remarks>
public class PhoneAssignmentService(IPhoneAssignmentManager phoneAssignmentManager) : IPhoneAssignmentService
{
    #region Fields

    private readonly IPhoneAssignmentManager _phoneAssignmentManager = phoneAssignmentManager;

    #endregion

    #region Methods

    /// <summary>
    /// Gets the current phone assignments for the active shift.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of <see cref="PhoneAssignmentDto"/> for the active shift.</returns>
    public async Task<IEnumerable<PhoneAssignmentDto>> GetCurrentPhoneAssignmentsForActiveShiftAsync(CancellationToken cancellationToken)
    {
        return await _phoneAssignmentManager.GetCurrentPhoneAssignmentsForActiveShift(cancellationToken);
    }

    #endregion
}
