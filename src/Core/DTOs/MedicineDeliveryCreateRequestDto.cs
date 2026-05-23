// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class MedicineDeliveryCreateRequestDto
{
    [Required]
    public Guid ResidentId { get; set; }

    [Required]
    [MaxLength(100)]
    public string MedicineName { get; set; } = string.Empty;

    [Required]
    public DateTime Timestamp { get; set; }

    public bool Given { get; set; }
}
