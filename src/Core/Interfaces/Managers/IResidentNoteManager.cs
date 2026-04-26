// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

namespace Core.Interfaces.Managers;

/// <summary>
/// Defines a contract for communicating with the ResidentNote API endpoints over HTTP.
/// </summary>
/// <remarks>
/// Implemented in Infrastructure. Separates HTTP communication concerns from business logic in Core,
/// following Clean Architecture — Core defines the contract, Infrastructure fulfils it.
/// </remarks>
/// <example>
/// <code>
/// // Registered in Infrastructure DependencyInjection:
/// services.AddScoped&lt;IResidentNoteManager, ResidentNoteManager&gt;();
/// </code>
/// </example>
public interface IResidentNoteManager
{
    #region Methods

    /// <summary>
    /// Retrieves all notes for a specific resident.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An enumerable collection of <see cref="ResidentNoteDto"/> for the specified resident.</returns>
    Task<IEnumerable<ResidentNoteDto>> GetAllByResidentIdAsync(Guid residentId, CancellationToken cancellationToken);

    /// <summary>
    /// Adds a new note for a resident.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <param name="noteText">A string containing the note text.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the note was added successfully; otherwise, <see langword="false"/>.</returns>
    Task<bool> AddAsync(Guid residentId, string noteText, CancellationToken cancellationToken);

    /// <summary>
    /// Updates the text of an existing note.
    /// </summary>
    /// <param name="noteId">A unique identifier for the note to update.</param>
    /// <param name="newText">A string containing the new note text.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the note was updated successfully; otherwise, <see langword="false"/>.</returns>
    Task<bool> UpdateAsync(Guid noteId, string newText, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a note by its unique identifier.
    /// </summary>
    /// <param name="noteId">A unique identifier for the note to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the note was deleted successfully; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteAsync(Guid noteId, CancellationToken cancellationToken);

    #endregion
}
