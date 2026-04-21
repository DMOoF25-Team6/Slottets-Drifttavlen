// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Data.Persistent;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

/// <summary>
/// Repository for managing phone assignments in the database.
/// </summary>
public class PhoneAssignmentRepository(AppDbContext dbContext)
    : Repository<PhoneAssignment>(dbContext), IPhoneAssignmentRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    /// <inheritdoc/>
    public async Task<IEnumerable<PhoneAssignment>> GetByShiftTypeAsync(
        string shiftType, CancellationToken cancellationToken)
    {
        return await _dbContext.PhoneAssignments
            .Where(p => p.ShiftType == shiftType)
            .ToListAsync(cancellationToken);
    }
}
