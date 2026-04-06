// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.
using Core.DTOs;
using Core.Interfaces.Repositories;
using Core.Mappers;

using Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[ApiController]
[Route("[controller]")]
public class MedicineController(IMedicineRepository medicineRepository, IPainkillerRepository painkillerRepository) : Controller
{
    private readonly IMedicineRepository _medicineRepository = medicineRepository;
    private readonly IPainkillerRepository _painkillerRepository = painkillerRepository;

    // GET: api/medicine/{residentId}
    [HttpGet("{residentId}")]
    public async Task<ActionResult<MedicineStatusDto>> GetMedicineStatus(Guid residentId)
    {
        IEnumerable<MedicineRecord> result = await _medicineRepository.GetMedicineStatusLast24HoursAsync(residentId);
        MedicineStatusDto medicineStatusDto = MedicineMapper.ToMedicineStatusDto(residentId, result);
        return Ok(medicineStatusDto);
    }

    // GET: api/medicine/painkiller/{residentId}
    [HttpGet("painkiller/{residentId}")]
    public async Task<ActionResult<PainkillerStatusDto>> GetPainkillerStatus(Guid residentId)
    {
        IEnumerable<PainkillerRecord> result = await _painkillerRepository.GetPainkillerStatusLast24HoursAsync(residentId);
        PainkillerStatusDto painkillerStatusDto = new()
        {
            ResidentId = residentId,
            Types = [.. result.Select(p => p.Type)],
            NextAllowedTime = result.Any() ? result.Max(p => p.NextAllowedTime).AddHours(4) : DateTime.UtcNow
        };
        return Ok(painkillerStatusDto);
    }
}
