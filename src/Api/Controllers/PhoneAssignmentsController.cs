// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Managers;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// API controller for managing phone assignments on the dashboard.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PhoneAssignmentsController(IPhoneAssignmentManager phoneAssignmentManager) : ControllerBase
{
    private readonly IPhoneAssignmentManager _phoneAssignmentManager = phoneAssignmentManager;

    /// <summary>
    /// Retrieves all phone assignments for the active shift.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of phone assignments for the active shift.</returns>
    [HttpGet("active-shift")]
    public async Task<ActionResult<IEnumerable<PhoneAssignmentDto>>> GetCurrentPhoneAssignmentsForActiveShift(
        CancellationToken cancellationToken)
    {
        IEnumerable<PhoneAssignmentDto> result = await _phoneAssignmentManager
            .GetCurrentPhoneAssignmentsForActiveShift(cancellationToken);
        return Ok(result);
    }
}
