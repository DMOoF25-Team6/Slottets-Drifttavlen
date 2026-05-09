// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Enums;


namespace Core.DTOs;

/// <summary>
/// Data transfer object used for creating or transferring staff assignment data.
/// </summary>

public class StaffAssignmentDto
{
    // The unique identifier of the resident to whom the staff member is assigned.
    public Guid ResidentId { get; set; }

    // ID of the assigned employee
    public Guid EmployeeId { get; set; }

    // The type of shift assigned to the staff member (e.g., Morning, Afternoon, Night).
    public ShiftType ShiftType { get; set; }

    // The date on which the staff member is assigned to the resident.
    public DateTime AssignmentDate { get; set; }


}
