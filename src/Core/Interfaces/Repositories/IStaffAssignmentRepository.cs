// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;
using Domain.Enums;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Repository interface for managing staff assignment data.
/// Provides methods for retrieving and checking staff assignments.
/// </summary>
/// 


/// <summary>
/// Retrieves all staff assignments for a specific resident.
/// </summary>
/// <param name="residentId">
/// The unique identifier of the resident.
/// </param>
/// <param name="cancellationToken">
/// Token used to cancel the asynchronous operation.
/// </param>
/// <returns>
/// A collection of staff assignments related to the specified resident.
/// </returns>

public interface IStaffAssignmentRepository: IRepository<StaffAssignment>
{

    // Get all assignments for a resident
    Task<IEnumerable<StaffAssignment>> GetByResidentAsync(
      Guid residentId,
      CancellationToken cancellationToken = default);

    // Get all assignments for a shift type and date
    Task<IEnumerable<StaffAssignment>> GetByShiftAsync(
        ShiftType shiftType,
        DateTime assignmentDate,
        CancellationToken cancellationToken = default);

    // Check if an assignment already exists for a resident, employee, shift type, and date
    Task<StaffAssignment?> GetExistingAssignmentAsync(
        Guid residentId,
        Guid employeeId,
        ShiftType shiftType,
        DateTime assignmentDate,
        CancellationToken cancellationToken = default);


    //Get an assignment by ID including related resident and employee details
    Task<StaffAssignment?> GetByIdWithDetailsAsync(
        Guid assignmentId,
        CancellationToken cancellationToken= default);

        

}
