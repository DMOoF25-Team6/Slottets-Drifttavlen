// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Services;

using Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// API controller for managing resident notes.
/// </summary>
/// <remarks>
/// Provides endpoints to retrieve, add, update, and delete notes for residents.
/// </remarks>
/// <example>
/// <code language="csharp">
/// // Example usage in HTTP client:
/// // GET /api/residentnote/{residentId}
/// // POST /api/residentnote
/// // PUT /api/residentnote/{residentId}/{noteId}
/// // DELETE /api/residentnote/{residentId}/{noteId}
/// </code>
/// </example>
[ApiController]
[Route("api/[controller]")]
public class ResidentNoteController : ControllerBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResidentNoteController"/> class.
    /// </summary>
    /// <param name="residentNoteService">The resident note service to use for note operations.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="residentNoteService"/> parameter is <see langword="null"/>.</exception>
    public ResidentNoteController(IResidentNoteService residentNoteService)
    {
        // Dependency injection for testability and separation of concerns
        ArgumentNullException.ThrowIfNull(residentNoteService);
        _residentNoteService = residentNoteService;
    }

    private readonly IResidentNoteService _residentNoteService;

    /// <summary>
    /// Retrieves all notes for a specific resident.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>An <see cref="IActionResult"/> containing the list of notes for the resident.</returns>
    /// <remarks>GET /api/residentnote/{residentId}</remarks>
    [HttpGet("{residentId}")]
    public async Task<IActionResult> GetAllByResidentId(Guid residentId, CancellationToken cancellationToken)
    {
        IEnumerable<ResidentNote> notes = await _residentNoteService.GetAllByResidentIdAsync(residentId, cancellationToken);
        return Ok(notes);
    }

    /// <summary>
    /// Adds a new note for a resident.
    /// </summary>
    /// <param name="dto">The data transfer object containing the resident ID and note text.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>An <see cref="IActionResult"/> indicating whether the operation was successful.</returns>
    /// <remarks>POST /api/residentnote</remarks>
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddResidentNoteDto dto, CancellationToken cancellationToken)
    {
        bool success = await _residentNoteService.AddAsync(dto.ResidentId, dto.NoteText, cancellationToken);
        return Ok(success);
    }

    /// <summary>
    /// Updates the text of an existing note for a resident.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <param name="noteId">A unique identifier for the note to update.</param>
    /// <param name="newText">The new text for the note.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>An <see cref="IActionResult"/> indicating whether the update was successful.</returns>
    /// <remarks>PUT /api/residentnote/{residentId}/{noteId}</remarks>
    [HttpPut("{residentId}/{noteId}")]
    public async Task<IActionResult> Update(Guid residentId, Guid noteId, [FromBody] string newText, CancellationToken cancellationToken)
    {
        bool success = await _residentNoteService.UpdateAsync(residentId, noteId, newText, cancellationToken);
        return Ok(success);
    }

    /// <summary>
    /// Deletes a note for a resident.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <param name="noteId">A unique identifier for the note to delete.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>An <see cref="IActionResult"/> indicating whether the deletion was successful.</returns>
    /// <remarks>DELETE /api/residentnote/{residentId}/{noteId}</remarks>
    [HttpDelete("{residentId}/{noteId}")]
    public async Task<IActionResult> Delete(Guid residentId, Guid noteId, CancellationToken cancellationToken)
    {
        bool success = await _residentNoteService.DeleteAsync(residentId, noteId, cancellationToken);
        return Ok(success);
    }
}
