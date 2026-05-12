// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Sar;

/// <summary>
/// Marks a SAR as fulfilled with audit timestamp (UC-010).
/// </summary>
public class SarFulfilledDto
{
    public Guid SarId { get; set; }
    public DateTime FulfilledAt { get; set; }
}
