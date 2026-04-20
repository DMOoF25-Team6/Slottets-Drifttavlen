// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.
using Core.DTOs;
using Core.Interfaces.Repositories;
using Core.Mappers;

using Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


/// <summary>
/// Handles API requests related to medicine and painkiller status for residents.
/// </summary>
/// <remarks>
/// Provides endpoints for retrieving medicine and painkiller status for a specific resident.
/// </remarks>
/// <summary>
/// Handles API requests related to medicine and painkiller status for residents.
/// </summary>
/// <remarks>
/// Provides endpoints for retrieving medicine and painkiller status for a specific resident.
/// </remarks>
[ApiController]
[Route("[controller]")]
public class MedicineController(IMedicineRepository medicineRepository, IPainkillerRepository painkillerRepository) : Controller
{
    private readonly IMedicineRepository _medicineRepository = medicineRepository;
    private readonly IPainkillerRepository _painkillerRepository = painkillerRepository;


    /// <summary>
    /// Retrieves the medicine status for a resident for the last 24 hours.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <returns>An <see cref="ActionResult{T}"/> containing the <see cref="MedicineStatusDto"/> for the resident.</returns>
    [HttpGet("{residentId}")]
    public async Task<ActionResult<MedicineStatusDto>> GetMedicineStatus(Guid residentId)
    {
        IEnumerable<MedicineRecord> result = await _medicineRepository.GetMedicineStatusLast24HoursAsync(residentId);
        MedicineStatusDto medicineStatusDto = MedicineMapper.ToMedicineStatusDto(residentId, result);
        return Ok(medicineStatusDto);
    }


    /// <summary>
    /// Retrieves the painkiller status for a resident for the last 24 hours.
    /// </summary>
    /// <param name="residentId">A unique identifier for the resident.</param>
    /// <returns>An <see cref="ActionResult{T}"/> containing the <see cref="PainkillerStatusDto"/> for the resident.</returns>
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
