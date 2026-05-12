// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs.Retention;

/// <summary>
/// Audit record of a retention policy change (UC-010).
/// </summary>
public class RetentionPolicyAuditDto
{
    public Guid Id { get; set; }
    public Guid RetentionPolicyId { get; set; }
    public Guid ChangedByEmployeeId { get; set; }
    public TimeSpan PreviousPeriod { get; set; }
    public TimeSpan NewPeriod { get; set; }
    public DateTime ChangedAt { get; set; }
    public string Reason { get; set; } = string.Empty;
}
