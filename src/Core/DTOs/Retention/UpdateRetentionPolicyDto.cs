// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Enums;

namespace Core.DTOs.Retention;

/// <summary>
/// Request payload for updating a retention policy (UC-010).
/// </summary>
public class UpdateRetentionPolicyDto
{
    public RetentionDataCategory Category { get; set; }
    public TimeSpan RetentionPeriod { get; set; }
    public string Reason { get; set; } = string.Empty;
}
