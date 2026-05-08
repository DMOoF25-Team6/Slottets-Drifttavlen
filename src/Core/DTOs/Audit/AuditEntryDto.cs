// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

namespace Core.DTOs.Audit;

/// <summary>
/// Represents an audit entry exposed to the UI for UC-009 (View History and Traceability).
/// </summary>
/// <remarks>
/// Provides a chronological record of a single auditable change, including who performed it,
/// what entity was affected, and when. Optionally includes the field-level change details
/// when a user drills down into a specific entry.
/// </remarks>
public class AuditEntryDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the audit entry.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the change took place.
    /// </summary>
    public DateTime EventTimeUtc { get; set; }

    /// <summary>
    /// Gets or sets the UTC timestamp when the change was registered (persisted).
    /// </summary>
    public DateTime RegisteredTimeUtc { get; set; }

    /// <summary>
    /// Gets or sets the name of the entity that was changed (e.g. "Resident", "ResidentNote").
    /// </summary>
    public string Entity { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of change (e.g. "Added", "Modified", "Deleted").
    /// </summary>
    public string ChangeType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a human-readable description of the change.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier of the user who performed the change.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the username of the user who performed the change, when available.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the change was saved successfully.
    /// </summary>
    public bool Succeeded { get; set; }

    /// <summary>
    /// Gets or sets the field-level change details associated with this audit entry.
    /// </summary>
    /// <remarks>
    /// Empty when the entry is shown in the chronological list; populated when the user
    /// drills down into a specific entry to view the change details.
    /// </remarks>
    public IEnumerable<ChangeDetailDto> ChangeDetails { get; set; } = [];
}
