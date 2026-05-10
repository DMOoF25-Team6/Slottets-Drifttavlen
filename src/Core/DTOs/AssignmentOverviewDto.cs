// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Core.DTOs;

/// <summary>
/// Data transfer object used for displaying staff assignment overview information.
/// </summary>
public class AssignmentOverviewDto
{
    // The ID of the staff assignment.
    public Guid AssignmentId { get; set; }

    // Id of the resident to whom the staff member is assigned
    public Guid ResidentId { get; set; }

    // Resident initials for display purposes
    public string ResidentInitials { get; set; } = string.Empty;

    // ID of the assigned employee
    public Guid EmployeeId { get; set; }

    // Name of the assigned employee for display purposes
    public string EmployeeName { get; set; } = string.Empty;

    // The type of shift assigned to the staff member (e.g., Morning, Afternoon, Night).
    public string ShiftType { get; set; } = string.Empty;

    // The date on which the staff member is assigned to the resident.
    public DateTime AssignmentDate { get; set; }


}
