// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

namespace Core.DTOs.Audit;

/// <summary>
/// Represents a single field-level change associated with an audit entry.
/// </summary>
/// <remarks>
/// Used by UC-009 (View History and Traceability) to display before/after values
/// when a user drills down into a specific audit entry.
/// </remarks>
public class ChangeDetailDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the change detail.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the field that was changed.
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value of the field before the change.
    /// </summary>
    public string OldValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value of the field after the change.
    /// </summary>
    public string NewValue { get; set; } = string.Empty;
}
