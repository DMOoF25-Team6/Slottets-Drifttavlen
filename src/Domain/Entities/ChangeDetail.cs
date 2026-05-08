// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// Represents a single field-level change recorded as part of an <see cref="AuditEntry"/>.
/// </summary>
/// <remarks>
/// Used for UC-009 (View History and Traceability) to provide field-by-field detail
/// of what was changed during an auditable operation. Each change detail belongs to
/// exactly one <see cref="AuditEntry"/>.
/// </remarks>
public class ChangeDetail : IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the change detail.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the field that was changed.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value of the field before the change.
    /// </summary>
    [MaxLength(2000)]
    public string OldValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value of the field after the change.
    /// </summary>
    [MaxLength(2000)]
    public string NewValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the foreign key to the parent <see cref="AuditEntry"/>.
    /// </summary>
    [Required]
    [ForeignKey(nameof(AuditEntry))]
    public Guid AuditEntryId { get; set; }
}
