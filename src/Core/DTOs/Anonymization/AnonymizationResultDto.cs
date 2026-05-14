// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Anonymization;

/// <summary>
/// Result of an anonymization operation (UC-010).
/// </summary>
public class AnonymizationResultDto
{
    public Guid CandidateId { get; set; }
    public DateTime CompletedAt { get; set; }
    public string Outcome { get; set; } = string.Empty;
}
