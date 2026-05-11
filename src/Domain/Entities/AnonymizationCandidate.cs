// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// Represents a resident whose data has reached the retention threshold and awaits
/// Admin review for anonymization or pseudonymization (UC-010).
/// </summary>
/// <remarks>
/// Created by RetentionBackgroundService, not by direct user action.
/// MedicineLogs are pseudonymized (not fully anonymized) per Autorisationsloven §22.
/// </remarks>
public class AnonymizationCandidate : IEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(Resident))]
    public Guid ResidentId { get; set; }

    [Required]
    [ForeignKey(nameof(RetentionPolicy))]
    public Guid RetentionPolicyId { get; set; }

    [Required]
    public DateTime SuggestedAt { get; set; }

    [Required]
    [MaxLength(500)]
    public string Reason { get; set; } = string.Empty;

    [Required]
    public AnonymizationStatus Status { get; set; } = AnonymizationStatus.Pending;

    public virtual Resident? Resident { get; set; }
    public virtual RetentionPolicy? RetentionPolicy { get; set; }
}
