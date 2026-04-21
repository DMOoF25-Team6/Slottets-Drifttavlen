// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Domain.Entities;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Defines a repository interface for managing phone assignments.
/// </summary>
public interface IPhoneAssignmentRepository : IRepository<PhoneAssignment>
{
    /// <summary>
    /// Retrieves all phone assignments for the active shift.
    /// </summary>
    /// <param name="shiftType">The shift type (Day, Evening, Night).</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of phone assignments for the given shift.</returns>
    Task<IEnumerable<PhoneAssignment>> GetByShiftTypeAsync(string shiftType, CancellationToken cancellationToken);
}
