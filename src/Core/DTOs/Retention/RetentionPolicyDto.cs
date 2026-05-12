// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Enums;

namespace Core.DTOs.Retention;

/// <summary>
/// Data transfer object representing a GDPR retention policy (UC-010).
/// </summary>
public class RetentionPolicyDto
{
    public Guid Id { get; set; }
    public RetentionDataCategory Category { get; set; }
    public TimeSpan RetentionPeriod { get; set; }
    public TimeSpan LegalMinimum { get; set; }
    public DateTime EffectiveFrom { get; set; }
}
