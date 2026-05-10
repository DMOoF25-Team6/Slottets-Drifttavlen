// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.DTOs;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services;

/// <summary>
/// Service for creating, retrieving, and deleting staff assignments.
/// </summary>

public class StaffAssignmentService(
    IStaffAssignmentRepository repository)
    : IStaffAssignmentService

{
    // Repository used to access staff assignment data
    private readonly IStaffAssignmentRepository _repository = repository;

    // Assigns an employee to a resident for a specific shift and date
    public async Task<AssignmentOverviewDto> AssignAsync(
      StaffAssignmentDto dto,
      CancellationToken cancellationToken = default)
    {

        // Check if the same assignment already exists
        StaffAssignment? existing =
            await _repository.GetExistingAssignmentAsync(
                dto.ResidentId,
                dto.EmployeeId,
                dto.ShiftType,
                dto.AssignmentDate,
                cancellationToken);

        // Stop if duplicate assignment is found
        if (existing is not null)
        {
            throw new InvalidOperationException("Assignment already exists.");
        }

        // Create a new StaffAssignment entity
        StaffAssignment assignment = new()
        {
            Id = Guid.NewGuid(),
            ResidentId = dto.ResidentId,
            EmployeeId = dto.EmployeeId,
            ShiftType = dto.ShiftType,
            AssignmentDate = dto.AssignmentDate.Date,
            CreatedAt = DateTime.UtcNow
        };

        // Save the new assignment to the database
        StaffAssignment created =
            await _repository.CreateAsync(
                assignment,
                cancellationToken);

        // Get created assignment with resident and employee details
        StaffAssignment? createdWithDetails =
            await _repository.GetByIdWithDetailsAsync(
                created.Id,
                cancellationToken);

        // Stop if created assignment cannot be found
        if (createdWithDetails is null)
        {
            throw new KeyNotFoundException("Assignment not found after creation.");
        }

        // Convert entity to DTO and return it
        return MapToOverviewDto(createdWithDetails);
    }



    // Get assignments by shift type and assignment date
    public async Task<IEnumerable<AssignmentOverviewDto>>
        GetAssignmentsByShiftAsync(
            ShiftType shiftType,
            DateTime assignmentDate,
            CancellationToken cancellationToken = default)
    {
        // Retrieve matching assignments from the database
        IEnumerable<StaffAssignment> assignments =
            await _repository.GetByShiftAsync(
                shiftType,
                assignmentDate,
                cancellationToken);

        return assignments.Select(MapToOverviewDto);
    }

    // Delete an assignment by ID
    public async Task DeleteAssignmentAsync(
        Guid assignmentId,
        CancellationToken cancellationToken = default)
    {
        // Find the assignment by ID
        StaffAssignment? assignment =
            await _repository.GetByIdAsync(
                assignmentId,
                cancellationToken);

        if (assignment is null)
        {
            throw new KeyNotFoundException("Assignment not found.");
        }

        await _repository.DeleteAsync(
            assignment,
            cancellationToken);
    }

    // Update an existing assignment
    public async Task<AssignmentOverviewDto> UpdateAssignmentAsync(
        Guid assignmentId,
        StaffAssignmentDto dto,
        CancellationToken cancellationToken = default)
    {
        // Find the assignment by ID
        StaffAssignment? assignment =
            await _repository.GetByIdAsync(
                assignmentId,
                cancellationToken);

        // Stop if assignment does not exist
        if (assignment is null)
        {
            throw new KeyNotFoundException("Assignment not found.");
        }

        assignment.ResidentId = dto.ResidentId;
        assignment.EmployeeId = dto.EmployeeId;
        assignment.ShiftType = dto.ShiftType;
        assignment.AssignmentDate = dto.AssignmentDate.Date;
        assignment.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(
            assignment,
            cancellationToken);

        StaffAssignment? updatedWithDetails =
            await _repository.GetByIdWithDetailsAsync(
                assignment.Id,
                cancellationToken);

        if (updatedWithDetails is null)
        {
            throw new KeyNotFoundException("Assignment not found after update.");
        }

        return MapToOverviewDto(updatedWithDetails);
    }

    // Convert StaffAssignment entity to AssignmentOverviewDto
    private static AssignmentOverviewDto MapToOverviewDto(
        StaffAssignment assignment)
    {
        // Create and return DTO for API response
        return new AssignmentOverviewDto
        {
            AssignmentId = assignment.Id,
            ResidentId = assignment.ResidentId,
            ResidentInitials = assignment.Resident?.Initials ?? string.Empty,
            EmployeeId = assignment.EmployeeId,
            EmployeeName = $"{assignment.Employee?.FirstName} {assignment.Employee?.LastName}".Trim(),
            ShiftType = assignment.ShiftType.ToString(),
            AssignmentDate = assignment.AssignmentDate
        };
    }
}