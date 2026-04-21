// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.Interfaces.Services;

/// <summary>
/// Defines a contract for managing resident notes in the system.
/// </summary>
/// <remarks>
/// Provides methods for adding, updating, deleting, and retrieving notes associated with residents.
/// </remarks>
public interface IResidentNoteService
{
    /// <summary>
    /// Adds a new note for a resident.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <param name="noteText">A string containing the note text.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the note was added successfully; otherwise, <see langword="false"/>.</returns>
    Task<bool> AddAsync(Guid residentId, string noteText, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a note by its unique identifier.
    /// </summary>
    /// <param name="noteId">A unique identifier for the note to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the note was deleted successfully; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteAsync(Guid noteId, CancellationToken cancellationToken);

    /// <summary>
    /// Updates the text of an existing note.
    /// </summary>
    /// <param name="noteId">A unique identifier for the note to update.</param>
    /// <param name="newText">A string containing the new note text.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the note was updated successfully; otherwise, <see langword="false"/>.</returns>
    Task<bool> UpdateAsync(Guid noteId, string newText, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all notes for a specific resident.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An enumerable collection of <see cref="Core.DTOs.ResidentNoteDto"/> for the specified resident.</returns>
    Task<IEnumerable<Core.DTOs.ResidentNoteDto>> GetAllByResidentIdAsync(Guid residentId, CancellationToken cancellationToken);
}
