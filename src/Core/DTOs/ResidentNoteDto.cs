// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs;

/// <summary>
/// Represents a data transfer object for sending resident note data to the client dashboard.
/// </summary>
/// <remarks>
/// Contains only the information needed by the frontend for displaying resident notes.
/// </remarks>
public class ResidentNoteDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the resident note.
    /// </summary>
    /// <value>
    /// A unique identifier for the note.
    /// </value>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the text content of the resident note.
    /// </summary>
    /// <value>
    /// The note text. The default is <see cref="string.Empty"/>.
    /// </value>
    public string Note { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the timestamp when the note was created or last modified.
    /// </summary>
    /// <value>
    /// The <see cref="DateTime"/> value representing when the note was created or last updated.
    /// </value>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the initials of the user who created or modified the note.
    /// </summary>
    /// <value>
    /// The initials of the user. The default is <see cref="string.Empty"/>.
    /// </value>
    public string Initials { get; set; } = string.Empty;
}
