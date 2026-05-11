// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Anonymization;

/// <summary>
/// Request payload for approving anonymization of a candidate (UC-010).
/// </summary>
public class ApproveAnonymizationDto
{
    public Guid CandidateId { get; set; }
}
