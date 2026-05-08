// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;

namespace Core.Services;

/// <summary>
/// Provides business logic operations for managing resident notes.
/// </summary>
/// <remarks>
/// Delegates all data access to <see cref="IResidentNoteManager"/> in Infrastructure,
/// ensuring Core never depends on Infrastructure or data access concerns directly —
/// following Clean Architecture and the Dependency Inversion Principle.
/// </remarks>
/// <example>
/// <code>
/// // Registered in Core DependencyInjection:
/// services.AddScoped&lt;IResidentNoteService, ResidentNoteService&gt;();
/// </code>
/// </example>
public class ResidentNoteService(IResidentNoteManager residentNoteManager) : IResidentNoteService
{
    #region Fields

    private readonly IResidentNoteManager _residentNoteManager = residentNoteManager;

    #endregion

    #region Methods

    /// <summary>
    /// Retrieves all notes for a specific resident.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An enumerable collection of <see cref="ResidentNoteDto"/> for the specified resident.</returns>
    public async Task<IEnumerable<ResidentNoteDto>> GetAllByResidentIdAsync(Guid residentId, CancellationToken cancellationToken = default)
    {
        return await _residentNoteManager.GetAllByResidentIdAsync(residentId, cancellationToken);
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
        return string.IsNullOrWhiteSpace(noteText)
            ? throw new ArgumentException("Note tekst må ikke være tom eller kun indeholde mellemrum.", nameof(noteText))
            : await _residentNoteManager.AddAsync(residentId, noteText, cancellationToken);
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
        return await _residentNoteManager.UpdateAsync(noteId, newText, cancellationToken);
    }

    /// <summary>
    /// Deletes a note by its unique identifier.
    /// </summary>
    /// <param name="noteId">A unique identifier for the note to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the note was deleted successfully; otherwise, <see langword="false"/>.</returns>
    public async Task<bool> DeleteAsync(Guid noteId, CancellationToken cancellationToken = default)
    {
        return await _residentNoteManager.DeleteAsync(noteId, cancellationToken);
    }

    #endregion
}
