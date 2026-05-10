// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Interfaces.Repositories;

using Domain.Entities;
using Domain.Enums;

using Infrastructure.Data.Persistent;

using Microsoft.EntityFrameworkCore;



namespace Infrastructure.Data.Repositories;

/// <summary>
/// Handles database queries for retrieving and validating staff assignments.
/// </summary>

public class StaffAssignmentRepository(AppDbContext context) : Repository<StaffAssignment>(context), IStaffAssignmentRepository
{
    // Get all assignments for a specific resident
    public async Task<IEnumerable<StaffAssignment>> GetByResidentAsync(
       Guid residentId,
       CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.Resident)
            .Include(a => a.Employee)
            .Where(a => a.ResidentId == residentId)
            .ToListAsync(cancellationToken);
    }


    // Get assignments by shift type and date
    public async Task<IEnumerable<StaffAssignment>> GetByShiftAsync(
        ShiftType shiftType,
        DateTime assignmentDate,
        CancellationToken cancellationToken = default)
    {
        // Remove time part from the date
        DateTime date = assignmentDate.Date;

        return await _dbSet
            .Include(a => a.Resident)
            .Include(a => a.Employee)
            .Where(a => a.ShiftType == shiftType && a.AssignmentDate.Date == date)
            .ToListAsync(cancellationToken);
    }

    // Check if an assignment already exists
    public async Task<StaffAssignment?> GetExistingAssignmentAsync(
        Guid residentId,
        Guid employeeId,
        ShiftType shiftType,
        DateTime assignmentDate,
        CancellationToken cancellationToken = default)
    {
        DateTime date = assignmentDate.Date;

        return await _dbSet

            // Return first matching assignment or null
            .FirstOrDefaultAsync(a =>
                a.ResidentId == residentId &&
                a.EmployeeId == employeeId &&
                a.ShiftType == shiftType &&
                a.AssignmentDate.Date == date,
                cancellationToken);
    }

    // Get assignment by ID including related resident and employee data
    public async Task<StaffAssignment?> GetByIdWithDetailsAsync(
    Guid assignmentId,
    CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.Resident)
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(a => a.Id == assignmentId, cancellationToken);
    }

}
