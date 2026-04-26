// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs;
using Core.Interfaces.Managers;

namespace Infrastructure.Managers;

/// <summary>
/// Provides operations for managing resident notes by communicating with the backend API over HTTP.
/// </summary>
/// <remarks>
/// Implements <see cref="IResidentNoteManager"/> using <see cref="HttpClient"/> to call the WebApi.
/// Keeps all HTTP communication out of Core — Core only knows the interface contract.
/// </remarks>
/// <example>
/// <code>
/// // Registered in Infrastructure DependencyInjection:
/// services.AddScoped&lt;IResidentNoteManager, ResidentNoteManager&gt;();
/// </code>
/// </example>
public class ResidentNoteManager : IResidentNoteManager
{
    #region Fields

    private readonly HttpClient _httpClient;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ResidentNoteManager"/> class.
    /// </summary>
    /// <param name="httpClientFactory">A factory used to create the named <see cref="HttpClient"/> for the API.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="httpClientFactory"/> parameter is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">The named HttpClient 'SlottetApi' could not be created.</exception>
    public ResidentNoteManager(IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);

        // Named client ensures correct BaseAddress and shared configuration across managers
        _httpClient = httpClientFactory.CreateClient("SlottetApi")
            ?? throw new InvalidOperationException("Failed to create HttpClient.");
    }

    #endregion

    #region Methods

    /// <summary>
    /// Retrieves all notes for a specific resident.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An enumerable collection of <see cref="ResidentNoteDto"/> for the specified resident.</returns>
    public async Task<IEnumerable<ResidentNoteDto>> GetAllByResidentIdAsync(Guid residentId, CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ResidentNoteDto>>(
            $"residentnote/{residentId}", cancellationToken) ?? [];
    }

    /// <summary>
    /// Adds a new note for a resident.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <param name="noteText">A string containing the note text.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the note was added successfully; otherwise, <see langword="false"/>.</returns>
    public async Task<bool> AddAsync(Guid residentId, string noteText, CancellationToken cancellationToken)
    {
        AddResidentNoteDto dto = new()
        {
            ResidentId = residentId,
            NoteText = noteText
        };

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("residentnote", dto, cancellationToken);

        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Updates the text of an existing note.
    /// </summary>
    /// <param name="noteId">A unique identifier for the note to update.</param>
    /// <param name="newText">A string containing the new note text.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the note was updated successfully; otherwise, <see langword="false"/>.</returns>
    public async Task<bool> UpdateAsync(Guid noteId, string newText, CancellationToken cancellationToken)
    {
        EditResidentNoteDto dto = new()
        {
            ResidentNoteId = noteId,
            NewNoteText = newText
        };

        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"residentnote/{noteId}", dto, cancellationToken);

        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Deletes a note by its unique identifier.
    /// </summary>
    /// <param name="noteId">A unique identifier for the note to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see langword="true"/> if the note was deleted successfully; otherwise, <see langword="false"/>.</returns>
    public async Task<bool> DeleteAsync(Guid noteId, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"residentnote/{noteId}", cancellationToken);

        return response.IsSuccessStatusCode;
    }

    #endregion
}
