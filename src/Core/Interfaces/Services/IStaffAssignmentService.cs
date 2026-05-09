// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.DTOs;

using Domain.Entities;
using Domain.Enums;


namespace Core.Interfaces.Services;


/// <summary>
/// Service interface for managing staff assignments.
/// </summary>
public interface IStaffAssignmentService
{

    // Create a new staff assignment
    Task<AssignmentOverviewDto> AssignAsync(
        StaffAssignmentDto dto,
        CancellationToken cancellationToken = default);



    // Get assignments by shift type and dat
    Task<IEnumerable<AssignmentOverviewDto>> GetAssignmentsByShiftAsync(
        ShiftType shiftType,
        DateTime assignmentDate,
        CancellationToken cancellationToken= default);
        
    // Delete assigment by ID
    Task DeleteAssignmentAsync(
        Guid assigmentId,
        CancellationToken cancellationToken= default);

    
    Task<AssignmentOverviewDto> UpdateAssignmentAsync(
    Guid assignmentId,
    StaffAssignmentDto dto,
    CancellationToken cancellationToken = default);
}
