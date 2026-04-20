// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


using Core.DTOs;
using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Data.Persistent;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;


/// <summary>
/// Handles API requests related to resident data.
/// </summary>
/// <remarks>
/// Provides endpoints for retrieving resident information and related notes.
/// </remarks>
[ApiController]
[Route("residents")]
public class ResidentController(IResidentRepository residentRepository) : ControllerBase
{
    private readonly IResidentRepository _residentRepository = residentRepository;

    /// <summary>
    /// Retrieves all residents and their associated notes.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="ActionResult{T}"/> containing a collection of <see cref="ResidentResponseDto"/>.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResidentResponseDto>>> GetAll(CancellationToken cancellationToken)
    {
        IEnumerable<Resident> residents = await _residentRepository.GetAllAsync(cancellationToken);
        IEnumerable<ResidentResponseDto> result = residents.Select(r => new ResidentResponseDto
        {
            Id = r.Id,
            Initials = r.Initials,
            TrafficLightStatus = r.TrafficLightStatus.HasValue ? (int)r.TrafficLightStatus.Value : null,
            Notes = [.. r.Notes.Select(n => new ResidentNoteDto
            {
                Id = n.Id,
                Note = n.Note,
                Timestamp = n.EditedAt,
                Initials = string.Empty
            })]
        });
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a resident by unique identifier, including their notes.
    /// </summary>
    /// <param name="id">A unique identifier for the resident.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="ActionResult{T}"/> containing the <see cref="ResidentResponseDto"/> if found; otherwise, <see cref="NotFoundResult"/>.</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ResidentResponseDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        Resident? resident = await _residentRepository.GetByIdAsync(id, cancellationToken);
        if (resident is null)
        {
            return NotFound();
        }
        ResidentResponseDto result = new()
        {
            Id = resident.Id,
            Initials = resident.Initials,
            TrafficLightStatus = resident.TrafficLightStatus.HasValue ? (int)resident.TrafficLightStatus.Value : null,
            Notes = [.. resident.Notes.Select(n => new ResidentNoteDto
            {
                Id = n.Id,
                Note = n.Note,
                Timestamp = n.EditedAt,
                Initials = string.Empty
            })]
        };
        return Ok(result);
    }

    /// <summary>
    /// Retrieves debug information about residents in the database.
    /// </summary>
    /// <param name="db">An instance of the application database context.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="IActionResult"/> containing the count and IDs of residents.</returns>
    [HttpGet("debug")]
    public async Task<IActionResult> Debug([FromServices] AppDbContext db, CancellationToken cancellationToken)
    {
        int count = await db.Set<Resident>().CountAsync(cancellationToken);
        var ids = await db.Set<Resident>()
            .Select(r => new { r.Id, r.Initials, r.TrafficLightStatus })
            .ToListAsync(cancellationToken);

        return Ok(new
        {
            count,
            ids
        });
    }
}
