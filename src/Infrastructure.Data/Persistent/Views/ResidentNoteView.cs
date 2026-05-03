// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

namespace Infrastructure.Data.Persistent.Views;

/// <summary>
/// Read model for the <c>vwResidentNote</c> database view.
/// Represents a resident note joined with the resident's initials.
/// </summary>
/// <remarks>
/// This is not a domain entity — it is a read-only projection used by the API
/// to return notes with display data (initials) without an extra join in code.
/// </remarks>
public sealed class ResidentNoteView
{
    public Guid Id { get; init; }
    public string Note { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime EditedAt { get; init; }
    public Guid ResidentId { get; init; }
    public string Initials { get; init; } = string.Empty;
}
