// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs;

public class MedicineDeliveryResponseDto
{
    public Guid Id { get; set; }
    public Guid ResidentId { get; set; }
    public string MedicineName { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public bool Given { get; set; }
}
