// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.
using Core.DTOs;
using Core.Interfaces.Services;

using Domain.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("staff-assignments")]
public class StaffAssignmentController(
    IStaffAssignmentService service)
    : ControllerBase
{
    private readonly IStaffAssignmentService _service = service;

    /// <summary>
    /// Assigns an employee to a resident for a shift.
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AssignmentOverviewDto>> CreateAssignment(
        [FromBody] StaffAssignmentDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            AssignmentOverviewDto result =
                await _service.AssignAsync(
                    dto,
                    cancellationToken);

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Gets all assignments for a shift and date.
    /// </summary>
    [Authorize]
    [HttpGet]

    public async Task<ActionResult<IEnumerable<AssignmentOverviewDto>>>
        GetAssignments(
         [FromQuery] ShiftType shiftType,
            [FromQuery] DateTime assignmentDate,
            CancellationToken cancellationToken)

    {

        IEnumerable<AssignmentOverviewDto> result =
           await _service.GetAssignmentsByShiftAsync(
               shiftType,
               assignmentDate,
               cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Deletes an assignment.
    /// </summary>

    [Authorize]
    [HttpDelete("{assignmentId:guid}")]
    public async Task<IActionResult> DeleteAssignment(
         Guid assignmentId,
        CancellationToken cancellationToken)
    {
        try
        {
            await _service.DeleteAssignmentAsync(
                assignmentId,
                cancellationToken);

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }

    }

    /// <summary>
    /// Updates an existing staff assignment.
    /// </summary>
    [Authorize]
    [HttpPut("{assignmentId:guid}")]
    public async Task<ActionResult<AssignmentOverviewDto>> UpdateAssignment(
        Guid assignmentId,
        [FromBody] StaffAssignmentDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            AssignmentOverviewDto result =
                await _service.UpdateAssignmentAsync(
                    assignmentId,
                    dto,
                    cancellationToken);

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

}




