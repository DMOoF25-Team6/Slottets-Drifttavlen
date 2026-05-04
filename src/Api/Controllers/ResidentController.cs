// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


using Core.DTOs;
using Core.DTOs.Identity;
using Core.Interfaces.Dto;
using Core.Interfaces.Repositories;

using Domain.Entities;

using Microsoft.AspNetCore.Mvc;

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
    /// Creates a new resident.
    /// </summary>
    /// <param name="dto">The resident creation data transfer object.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An <see cref="ActionResult{T}"/> containing the created <see cref="IResidentResult"/> and location header.</returns>
    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] ResidentCreateDto dto, CancellationToken cancellationToken)
    {
        if (dto is null)
        {
            ErrorDto Error = new()
            {
                ErrorMessages = ["Request body cannot be null."]
            };
            return BadRequest(Error);
        }

        Resident resident = new()
        {
            Id = Guid.NewGuid(),
            Initials = dto.Initials,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            TrafficLightStatus = dto.TrafficLightStatus
        };

        Resident created = await _residentRepository.CreateAsync(resident, cancellationToken);

        ResidentResponseDto response = new()
        {
            Id = created.Id,
            Initials = created.Initials,
            TrafficLightStatus = created.TrafficLightStatus.HasValue ? (int)created.TrafficLightStatus.Value : null,
            Notes = []
        };

        return Ok(response);
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
}
