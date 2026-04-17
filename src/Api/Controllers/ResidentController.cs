// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


using Microsoft.EntityFrameworkCore;


using Core.DTOs;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

using Domain.Entities;

using Infrastructure.Data.Persistent;
using Infrastructure.Data.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[ApiController]
[Route("residents")]
public class ResidentController(IResidentRepository residentRepository): ControllerBase
{
    private readonly IResidentRepository _residentRepository = residentRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResidentResponseDto>>> GetAll(CancellationToken cancellationToken)
    {
        IEnumerable<Resident> residents = await _residentRepository.GetAllAsync(cancellationToken);
        IEnumerable<ResidentResponseDto> result = residents.Select(r => new ResidentResponseDto
        {
            Id = r.Id,
            Initials = r.Initials,
            TrafficLightStatus = r.TrafficLightStatus.HasValue ? (int)r.TrafficLightStatus.Value : (int?)null,
            Notes = r.Notes.Select(n => new ResidentNoteDto
            {
                Id = n.Id,
                Note = n.Note,
                Timestamp = n.EditedAt,
                Initials = string.Empty
            }).ToList()
        });
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ResidentResponseDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        Resident? resident = await _residentRepository.GetByIdAsync(id, cancellationToken);
        if (resident is null)
        {
            return NotFound();
        }
        ResidentResponseDto result = new ResidentResponseDto
        {
            Id = resident.Id,
            Initials = resident.Initials,
            TrafficLightStatus = resident.TrafficLightStatus.HasValue ? (int)resident.TrafficLightStatus.Value : (int?)null,
            Notes = resident.Notes.Select(n => new ResidentNoteDto
            {
                Id = n.Id,
                Note = n.Note,
                Timestamp = n.EditedAt,
                Initials = string.Empty
            }).ToList()
        };
        return Ok(result);
    }

    [HttpGet("debug")]
    public async Task<IActionResult> Debug([FromServices] AppDbContext db, CancellationToken cancellationToken)
    {
        var count = await db.Set<Resident>().CountAsync(cancellationToken);
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
