// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;

using Domain.Interfaces;

namespace Domain.Entities;

public class PhoneAssignment : IEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid CaregiverId { get; set; }

    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string ShiftType { get; set; } = string.Empty;
}
