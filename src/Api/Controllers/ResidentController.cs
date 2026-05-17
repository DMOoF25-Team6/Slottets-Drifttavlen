// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.


using System.Security.Claims;

using Core.DTOs;
using Core.Interfaces.Repositories;

using Domain.Entities;
using Domain.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Handles API requests related to resident data.
/// </summary>
/// <remarks>
/// Provides endpoints for retrieving resident information and related notes.
/// </remarks>
[ApiController]
// UC-007: REQ-F-005 — All resident data requires authentication.
[Authorize]
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
            Department = r.Department,
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

    [HttpGet("department/{department}")]
    public async Task<ActionResult<IEnumerable<ResidentResponseDto>>> GetByDepartment(Department department, CancellationToken cancellationToken)
    {
        IEnumerable<Resident> residents = await _residentRepository.GetAllAsync(department, cancellationToken);
        IEnumerable<ResidentResponseDto> result = residents.Select(r => new ResidentResponseDto
        {
            Id = r.Id,
            Initials = r.Initials,
            TrafficLightStatus = r.TrafficLightStatus.HasValue ? (int)r.TrafficLightStatus.Value : null,
            Department = r.Department,
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
    [Authorize(Policy = "CanManageResidents")]
    [HttpPost("Create")]
    public async Task<ActionResult<ResidentResponseDto>> Create([FromBody] ResidentCreateRequestDto dto, CancellationToken cancellationToken)
    {
        if (dto is null)
        {
            ErrorDto Error = new()
            {
                ErrorMessages = ["Request body cannot be null."]
            };
            return BadRequest(Error);
        }

        if (!UserCanManageDepartment(dto.Department))
        {
            return Forbid();
        }

        Resident resident = new()
        {
            Id = Guid.NewGuid(),
            Initials = dto.Initials,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            TrafficLightStatus = dto.TrafficLightStatus,
            Department = dto.Department
        };

        Resident created = await _residentRepository.CreateAsync(resident, cancellationToken);

        ResidentResponseDto response = new()
        {
            Id = created.Id,
            Initials = created.Initials,
            TrafficLightStatus = created.TrafficLightStatus.HasValue ? (int)created.TrafficLightStatus.Value : null,
            Department = created.Department,
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
            Department = resident.Department,
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
    /// Updates an existing resident.
    /// </summary>
    /// <param name="id">The unique identifier of the resident to update.</param>
    /// <param name="dto">The updated resident data.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see cref="NoContentResult"/> on success; <see cref="NotFoundResult"/> if no resident with the given id exists; <see cref="ForbidResult"/> if the user's department does not match.</returns>
    [Authorize(Policy = "CanManageResidents")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] ResidentUpdateRequestDto dto, CancellationToken cancellationToken)
    {
        Resident? existing = await _residentRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return NotFound();
        }

        if (!UserCanManageDepartment(existing.Department))
        {
            return Forbid();
        }

        // Only allow department change for admins (no Department claim)
        ClaimsPrincipal? principal = HttpContext?.User;
        string? deptClaim = principal?.FindFirstValue("Department");
        if (!string.IsNullOrEmpty(deptClaim) && dto.Department != existing.Department)
        {
            // Non-admins cannot change department
            return Forbid();
        }

        if (!UserCanManageDepartment(dto.Department))
        {
            return Forbid();
        }

        existing.Initials = dto.Initials;
        existing.FirstName = dto.FirstName;
        existing.LastName = dto.LastName;
        existing.TrafficLightStatus = dto.TrafficLightStatus;
        existing.Department = dto.Department;

        await _residentRepository.UpdateAsync(existing, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Deletes a resident by unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the resident to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns><see cref="NoContentResult"/> on success; <see cref="NotFoundResult"/> if no resident with the given id exists; <see cref="ForbidResult"/> if the user's department does not match.</returns>
    [Authorize(Policy = "CanManageResidents")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        Resident? existing = await _residentRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return NotFound();
        }

        if (!UserCanManageDepartment(existing.Department))
        {
            return Forbid();
        }

        await _residentRepository.DeleteAsync(existing, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Marks a resident as discharged, starting the GDPR retention countdown (UC-010).
    /// </summary>
    /// <param name="id">The unique identifier of the resident to discharge.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The updated resident on success; <see cref="NotFoundResult"/> when the resident does not exist; <see cref="ConflictObjectResult"/> when the resident is already discharged.</returns>
    /// <remarks>
    /// Sets <c>Resident.DischargedAt = DateTime.UtcNow</c>. From that moment the
    /// <c>RetentionBackgroundService</c> uses this timestamp instead of the
    /// last-activity fallback when deciding whether to create an
    /// <c>AnonymizationCandidate</c> per GDPR Art. 5(1)(e) storage limitation.
    /// The operation is intentionally idempotent-on-state: a second discharge
    /// returns 409 Conflict so the original discharge date — and therefore the
    /// retention deadline — cannot be silently reset.
    /// </remarks>
    [Authorize(Policy = "CanManageResidents")]
    [HttpPost("{id:guid}/discharge")]
    public async Task<ActionResult<ResidentResponseDto>> Discharge(Guid id, CancellationToken cancellationToken)
    {
        Resident? resident = await _residentRepository.GetByIdAsync(id, cancellationToken);
        if (resident is null)
        {
            return NotFound();
        }

        if (!UserCanManageDepartment(resident.Department))
        {
            return Forbid();
        }

        if (resident.DischargedAt.HasValue)
        {
            return Conflict(new ErrorDto
            {
                ErrorMessages =
                [
                    $"Resident is already discharged at {resident.DischargedAt:O}; "
                    + "resetting the discharge date is not permitted (GDPR retention countdown integrity)."
                ]
            });
        }

        resident.DischargedAt = DateTime.UtcNow;
        await _residentRepository.UpdateAsync(resident, cancellationToken);

        ResidentResponseDto dto = new()
        {
            Id = resident.Id,
            Initials = resident.Initials,
            TrafficLightStatus = resident.TrafficLightStatus.HasValue ? (int)resident.TrafficLightStatus.Value : null,
            Department = resident.Department,
            Notes = []
        };
        return Ok(dto);
    }

    /// <summary>
    /// Returns <see langword="true"/> when the authenticated user may manage residents in <paramref name="department"/>.
    /// Users without a Department claim are unrestricted (admins). Users with a Department claim must match.
    /// </summary>
    private bool UserCanManageDepartment(Department department)
    {
        ClaimsPrincipal? principal = HttpContext?.User;
        string? deptClaim = principal?.FindFirstValue("Department");

        // No department claim → unrestricted (admin)
        return string.IsNullOrEmpty(deptClaim) || (Enum.TryParse<Department>(deptClaim, out Department userDept) && userDept == department);
    }
}
