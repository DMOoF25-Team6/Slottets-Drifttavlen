// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs;

/// <summary>
/// Represents a request to add a note for a specific resident.
/// </summary>
/// <remarks>
/// This DTO is used to transfer note data for a resident, including the resident's identifier and the note text.
/// </remarks>
public class AddResidentNoteDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the resident for whom the note is being added.
    /// </summary>
    /// <value>
    /// A unique identifier for the resident.
    /// </value>
    public Guid ResidentId { get; set; }

    /// <summary>
    /// Gets or sets the text of the note to be added for the resident.
    /// </summary>
    /// <value>
    /// The note text. The default is <see cref="string.Empty"/>.
    /// </value>
    public string NoteText { get; set; } = string.Empty;
}
