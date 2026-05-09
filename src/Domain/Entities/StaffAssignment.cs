// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Enums;
using Domain.Interfaces;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Entities;

/// <summary>
/// Represents a staff assignment connecting an employee to a resident
/// for a specific shift and date.
/// </summary>

public class StaffAssignment : IEntity
{

    //  Gets or sets the unique identifier for the staff assignment
    [Key]
    public Guid Id { get; set; }


    // Gets or sets the ID of the resident assigned to the employee.
    [Required]
    [ForeignKey(nameof(Resident))]
    public Guid ResidentId { get; set; }

    // Gets or sets the ID of the employee assigned to the resident.
    [Required]
    [ForeignKey(nameof(Employee))]
    public Guid EmployeeId { get; set; }

    // Gets or sets the type of shift for this assignment (Day, Evening, Night).
    [Required]
    public ShiftType ShiftType { get; set; }


    // Gets or sets the date of the assignment
    [Required]
    public DateTime AssignmentDate { get; set; }


    //Gets or sets the date and time when the assignment was created.
    [Required]
    public DateTime CreatedAt { get; set; }

    // Gets or sets the date and time when the assignment was last updated. 
    // This value can be null if the assignment has never been updated.
    public DateTime? UpdatedAt { get; set; }

    public virtual Resident? Resident { get; set; }
    public virtual Employee? Employee { get; set; }

}
