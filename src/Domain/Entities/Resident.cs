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
    public TrafficLightStatus? TrafficLightStatus { get; set; }
    public virtual ICollection<ResidentNote> Notes { get; set; } = [];
    public virtual ICollection<MedicineRecord> Medicines { get; set; } = [];
    public virtual ICollection<PainkillerRecord> Painkillers { get; set; } = [];
}