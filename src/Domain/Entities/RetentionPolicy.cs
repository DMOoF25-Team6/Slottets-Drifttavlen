// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;

using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// Represents a GDPR retention policy for a specific data category (UC-010).
/// </summary>
/// <remarks>
/// MedicineLogs has a locked legal minimum of 10 years per Autorisationsloven §22.
/// Other categories may be configured by Admin within reasonable bounds.
/// </remarks>
public class RetentionPolicy : IEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public RetentionDataCategory Category { get; set; }

    [Required]
    public TimeSpan RetentionPeriod { get; set; }

    [Required]
    public TimeSpan LegalMinimum { get; set; }

    [Required]
    public DateTime EffectiveFrom { get; set; }

    public virtual ICollection<RetentionPolicyAudit> AuditHistory { get; set; } = [];
    public virtual ICollection<AnonymizationCandidate> Candidates { get; set; } = [];
}
