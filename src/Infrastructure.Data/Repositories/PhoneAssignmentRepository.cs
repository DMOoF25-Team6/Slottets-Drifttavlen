// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Repositories;

using Domain.Entities;
using Domain.Enums;

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
        ShiftType shiftType, CancellationToken cancellationToken)
    {
        string shiftTypeString = shiftType.ToString();
        return await _dbContext.PhoneAssignments
            .Where(p => p.ShiftType == shiftTypeString)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<PhoneAssignmentDto>> GetDtoByShiftTypeAsync(
        ShiftType shiftType, CancellationToken cancellationToken)
    {
        string shiftTypeString = shiftType.ToString();
        return await _dbContext.PhoneAssignments
            .Where(p => p.ShiftType == shiftTypeString)
            .Select(p => new PhoneAssignmentDto
            {
                PhoneNumber = p.PhoneNumber,
                ShiftType = p.ShiftType,
                AssignedStaffName = _dbContext.Users
                    .Where(u => u.Id == p.CaregiverId)
                    .Select(u => u.UserName)
                    .FirstOrDefault() ?? string.Empty
            })
            .ToListAsync(cancellationToken);
    }
}
