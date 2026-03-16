// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;

using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities;

public class Resident : IEntity
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(2)]
    public string Initials { get; set; } = string.Empty;
    [Required]
    public TrafficLight? TrafficLight { get; set; }

    #region Navigation Properties
    public virtual ICollection<ResidentNote> Notes { get; set; } = [];
    #endregion
}
