// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.DTOs;

/// <summary>
/// Represents a request to edit an existing note for a specific resident.
/// </summary>
/// <remarks>
/// This DTO is used to transfer data for editing a resident's note, including the resident's identifier, the note's identifier, and the new note text.
/// </remarks>
public class EditResidentNoteDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the resident whose note is being edited.
    /// </summary>
    /// <value>
    /// A unique identifier for the resident.
    /// </value>
    public Guid ResidentId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the note to be edited.
    /// </summary>
    /// <value>
    /// A unique identifier for the resident's note.
    /// </value>
    public Guid ResidentNoteId { get; set; }

    /// <summary>
    /// Gets or sets the new text for the resident's note.
    /// </summary>
    /// <value>
    /// The new note text. The default is <see cref="string.Empty"/>.
    /// </value>
    public string NewNoteText { get; set; } = string.Empty;
}
