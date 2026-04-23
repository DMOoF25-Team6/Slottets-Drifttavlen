// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

using Domain.Entities;

namespace Core.Services;

/// <summary>
/// Provides operations for managing resident notes.
/// </summary>
/// <remarks>
/// Implements business logic for creating, updating, retrieving, and deleting resident notes.
/// </remarks>
public class ResidentNoteService(IResidentNoteRepository residentNoteRepository) : IResidentNoteService
{
    private readonly IResidentNoteRepository _residentNoteRepository = residentNoteRepository;

    /// <summary>
    /// Retrieves all notes for a specific resident.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An enumerable collection of <see cref="ResidentNoteDto"/> for the specified resident.</returns>
    public async Task<IEnumerable<ResidentNoteDto>> GetAllByResidentIdAsync(Guid residentId, CancellationToken cancellationToken = default)
    {
        IEnumerable<ResidentNote> allNotes = await _residentNoteRepository.GetAllAsync(cancellationToken);
        return allNotes
     .Where(n => n.ResidentId == residentId)
     .Select(n => new ResidentNoteDto
     {
         Id = n.Id,
         Note = n.Note,
         Timestamp = n.EditedAt,
         Initials = string.Empty
     });
    }

    /// <summary>
    /// Adds a new note for a resident.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <param name="noteText">A string containing the note text.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the note was added successfully; otherwise, <see langword="false"/>.</returns>
    public async Task<bool> AddAsync(Guid residentId, string noteText, CancellationToken cancellationToken = default)
    {
        ResidentNote note = new()
        {
            Id = Guid.NewGuid(),
            ResidentId = residentId,
            Note = noteText,
            EditedAt = DateTime.UtcNow
        };
        _ = await _residentNoteRepository.AddAsync(note, cancellationToken);
        return true;
    }

    /// <summary>
    /// Updates the text of an existing note.
    /// </summary>
    /// <param name="noteId">A unique identifier for the note to update.</param>
    /// <param name="newText">A string containing the new note text.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the note was updated successfully; otherwise, <see langword="false"/>.</returns>
    public async Task<bool> UpdateAsync(Guid noteId, string newText, CancellationToken cancellationToken = default)
    {
        // Step 1 - Fetch the note
        ResidentNote? note = await _residentNoteRepository.GetByIdAsync(noteId, cancellationToken);

        // Step 2 - Check if note exists
        if (note is null)
        {
            return false;
        }

        // Step 3 - Update fields
        note.Note = newText;
        note.EditedAt = DateTime.UtcNow;

        // Step 4 - Save via repository
        await _residentNoteRepository.UpdateAsync(note, cancellationToken);

        // Step 5 - Return success
        return true;
    }

    /// <summary>
    /// Deletes a note by its unique identifier.
    /// </summary>
    /// <param name="noteId">A unique identifier for the note to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the note was deleted successfully; otherwise, <see langword="false"/>.</returns>
    public async Task<bool> DeleteAsync(Guid noteId, CancellationToken cancellationToken = default)
    {
        // Step 1 - Fetch the note
        ResidentNote? note = await _residentNoteRepository.GetByIdAsync(noteId, cancellationToken);

        // Step 2 - Check if note exists
        if (note is null)
        {
            return false;
        }

        // Step 3 - Delete via repository
        await _residentNoteRepository.DeleteAsync(note, cancellationToken);

        // Step 4 - Return success
        return true;
    }

    //public Task<bool> UpdateAsync(Guid noteId, string newText, CancellationToken cancellationToken)
    //{
    //    throw new NotImplementedException();
    //}
}
