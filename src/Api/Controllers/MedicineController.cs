// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.
using Core.Handlers;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MedicineController : Controller
{
    private readonly MedicineStatusHandler _handler;

    public MedicineController(MedicineStatusHandler handler)
    {
        _handler = handler;
    }

    // GET: api/medicine/{residentId}
    [HttpGet("{residentId}")]
    public async Task<ActionResult<MedicineStatusDto>> GetMedicineStatus(Guid residentId)
    {
        var result = await _handler.GetMedicineStatusAsync(residentId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // GET: api/medicine/painkiller/{residentId}
    [HttpGet("painkiller/{residentId}")]
    public async Task<ActionResult<PainkillerStatusDto>> GetPainkillerStatus(Guid residentId)
    {
        var result = await _handler.GetPainkillerStatusAsync(residentId);
        return Ok(result);
    }


}
