// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.




using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

using Domain.Entities;

using Infrastructure.Data.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[ApiController]
[Route("residents")]
public class ResidentController(IResidentRepository residentRepository): ControllerBase
{
    private readonly IResidentRepository _residentRepository = residentRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Resident>>> GetAll(CancellationToken cancellationToken)
    {
        IEnumerable<Resident> residents = await _residentRepository.GetAllAsync(cancellationToken);
        return Ok(residents);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Resident>> GetById(Guid id, CancellationToken cancellationToken)
    {
        Resident? resident = await _residentRepository.GetByIdAsync(id, cancellationToken);
        if (resident == null)
        {
            return NotFound();
        }
        return Ok(resident);
    }
}
