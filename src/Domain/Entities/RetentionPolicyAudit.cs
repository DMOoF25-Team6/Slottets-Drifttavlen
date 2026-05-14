// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// Records every change to a RetentionPolicy with previous and new values (UC-010).
/// </summary>
/// <remarks>
/// Created automatically by RetentionPolicyManager when a policy is updated.
/// Provides full traceability for Datatilsynet inspection of retention configuration history.
/// </remarks>
public class RetentionPolicyAudit : IEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(RetentionPolicy))]
    public Guid RetentionPolicyId { get; set; }

    [Required]
    public Guid ChangedByEmployeeId { get; set; }

    [Required]
    public TimeSpan PreviousPeriod { get; set; }

    [Required]
    public TimeSpan NewPeriod { get; set; }

    [Required]
    public DateTime ChangedAt { get; set; }

    [MaxLength(500)]
    public string Reason { get; set; } = string.Empty;

    public virtual RetentionPolicy? RetentionPolicy { get; set; }
}
