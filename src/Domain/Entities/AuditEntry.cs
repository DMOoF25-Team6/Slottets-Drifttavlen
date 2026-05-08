// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// Represents an immutable audit log entry capturing a change to a domain entity.
/// </summary>
/// <remarks>
/// Created automatically by the <c>AuditInterceptor</c> on every successful <c>SaveChanges</c>
/// operation. Used by UC-009 (View History and Traceability) to display a chronological
/// history of changes. Field-level details are captured via the related
/// <see cref="ChangeDetails"/> collection.
/// </remarks>
public class AuditEntry : IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the audit entry.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets metadata describing the audited change (typically "EntityName - State").
    /// </summary>
    public string Metadata { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the UTC timestamp when the change started (before save).
    /// </summary>
    public DateTime StartTimeUtc { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the change completed (after save).
    /// </summary>
    public DateTime EndTimeUtc { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the change was saved successfully.
    /// </summary>
    public bool Succeeded { get; set; }

    /// <summary>
    /// Gets or sets the foreign key to the user who performed the change.
    /// </summary>
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the error message if the change failed; otherwise an empty string.
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the field-level change details for this audit entry.
    /// </summary>
    /// <remarks>
    /// Provides detailed before/after values for each modified field. Used by UC-009
    /// when the user drills down into a specific audit entry.
    /// </remarks>
    public virtual ICollection<ChangeDetail> ChangeDetails { get; set; } = [];
}
