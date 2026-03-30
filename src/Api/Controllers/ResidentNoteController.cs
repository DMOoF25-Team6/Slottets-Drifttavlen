// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Services;

using Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResidentNoteController : ControllerBase
{
    private readonly IResidentNoteService _residentNoteService;

    public ResidentNoteController(IResidentNoteService residentNoteService)
    {
        _residentNoteService = residentNoteService;
    }

    // GET /api/residentnote/{residentId}
    [HttpGet("{residentId}")]
    public async Task<IActionResult> GetAllByResidentId(Guid residentId, CancellationToken cancellationToken)
    {
        IEnumerable<ResidentNote> notes = await _residentNoteService.GetAllByResidentIdAsync(residentId, cancellationToken);
        return Ok(notes);
    }

    // POST /api/residentnote
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddResidentNoteDto dto, CancellationToken cancellationToken)
    {
        var success = await _residentNoteService.AddAsync(dto.ResidentId, dto.NoteText, cancellationToken);
        return Ok(success);
    }

    // PUT /api/residentnote/{residentId}/{noteId}
    [HttpPut("{residentId}/{noteId}")]
    public async Task<IActionResult> Update(Guid residentId, Guid noteId, [FromBody] string newText, CancellationToken cancellationToken)
    {
        var success = await _residentNoteService.UpdateAsync(residentId, noteId, newText, cancellationToken);
        return Ok(success);
    }

    // DELETE /api/residentnote/{residentId}/{noteId}
    [HttpDelete("{residentId}/{noteId}")]
    public async Task<IActionResult> Delete(Guid residentId, Guid noteId, CancellationToken cancellationToken)
    {
        var success = await _residentNoteService.DeleteAsync(residentId, noteId, cancellationToken);
        return Ok(success);
    }
}