// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities;

public class Employee : IEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    [StringLength(3)]
    public string Initials { get; set; } = string.Empty;
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    /// <summary>
    /// The department this employee is assigned to.
    /// </summary>
    public Department Department { get; set; }

    /// <summary>
    /// Navigation property to the associated Identity user.
    /// </summary>
    public virtual User? User { get; set; }
}
