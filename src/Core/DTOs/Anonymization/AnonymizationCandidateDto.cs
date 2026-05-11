// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Enums;

namespace Core.DTOs.Anonymization;

/// <summary>
/// Data transfer object representing an anonymization candidate awaiting review (UC-010).
/// </summary>
public class AnonymizationCandidateDto
{
    public Guid Id { get; set; }
    public Guid ResidentId { get; set; }
    public Guid RetentionPolicyId { get; set; }
    public DateTime SuggestedAt { get; set; }
    public string Reason { get; set; } = string.Empty;
    public AnonymizationStatus Status { get; set; }
}
