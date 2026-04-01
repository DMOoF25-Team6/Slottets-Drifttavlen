// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Interfaces;

namespace Domain.Entities;

public class MedicineRecord : IEntity
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey(nameof(Resident))]
    public Guid ResidentId { get; set; }
    [Required]
    [MaxLength(100)]
    public string MedicineName { get; set; } = string.Empty;
    [Required]
    public DateTime Timestamp { get; set; }
    [Required]
    public bool Given { get; set; } = false;
}
