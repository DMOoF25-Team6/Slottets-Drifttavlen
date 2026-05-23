// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Repositories;
using Core.Mappers;

using Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Handles administration of medicine deliveries (UC-020/021/022).
/// </summary>
/// <remarks>
/// Mirrors <c>ResidentController</c>: the API tier talks straight to the repository
/// (Infrastructure.Data) — the HTTP/Service/Manager layering lives on the client side.
/// </remarks>
[ApiController]
[Authorize(Roles = "admin")]
[Route("medicinedelivery")]
public class MedicineDeliveryController(IMedicineRepository medicineRepository) : ControllerBase
{
    private readonly IMedicineRepository _medicineRepository = medicineRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedicineDeliveryResponseDto>>> GetAll(CancellationToken cancellationToken)
    {
        IEnumerable<MedicineRecord> records = await _medicineRepository.GetAllAsync(cancellationToken);
        return Ok(records.Select(MedicineDeliveryMapper.ToResponseDto));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MedicineDeliveryResponseDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        MedicineRecord? record = await _medicineRepository.GetByIdAsync(id, cancellationToken);
        return record is null ? NotFound() : Ok(MedicineDeliveryMapper.ToResponseDto(record));
    }

    [HttpPost]
    public async Task<ActionResult<MedicineDeliveryResponseDto>> Create([FromBody] MedicineDeliveryCreateRequestDto dto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);
        MedicineRecord record = MedicineDeliveryMapper.ToMedicineRecord(dto);
        MedicineRecord created = await _medicineRepository.CreateAsync(record, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, MedicineDeliveryMapper.ToResponseDto(created));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] MedicineDeliveryUpdateRequestDto dto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);
        MedicineRecord? existing = await _medicineRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return NotFound();
        }
        MedicineDeliveryMapper.ApplyUpdate(existing, dto);
        await _medicineRepository.UpdateAsync(existing, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        MedicineRecord? existing = await _medicineRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return NotFound();
        }
        await _medicineRepository.DeleteAsync(existing, cancellationToken);
        return NoContent();
    }
}
