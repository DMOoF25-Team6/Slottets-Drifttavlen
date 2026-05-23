// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Authorize(Roles = "admin")]
[Route("[controller]")]
public class MedicineDeliveryController : Controller
{
    private readonly IMedicineDeliveryService _service;

    public MedicineDeliveryController(IMedicineDeliveryService service)
    {
        ArgumentNullException.ThrowIfNull(service);
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MedicineDeliveryResponseDto>>> GetAll(CancellationToken cancellationToken)
    {
        IEnumerable<MedicineDeliveryResponseDto> all = await _service.GetAllAsync(cancellationToken);
        return Ok(all);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MedicineDeliveryResponseDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        MedicineDeliveryResponseDto? dto = await _service.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<MedicineDeliveryResponseDto>> Create([FromBody] MedicineDeliveryCreateRequestDto dto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);
        MedicineDeliveryResponseDto created = await _service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] MedicineDeliveryUpdateRequestDto dto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(dto);
        bool ok = await _service.UpdateAsync(id, dto, cancellationToken);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        bool ok = await _service.DeleteAsync(id, cancellationToken);
        return ok ? NoContent() : NotFound();
    }
}
