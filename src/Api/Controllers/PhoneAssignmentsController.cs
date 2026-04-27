// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// API controller for managing phone assignments on the dashboard.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PhoneAssignmentsController(IPhoneAssignmentService phoneAssignmentService) : ControllerBase
{
    #region Fields

    private readonly IPhoneAssignmentService _phoneAssignmentService = phoneAssignmentService;

    #endregion

    #region Endpoints

    /// <summary>
    /// Retrieves all phone assignments for the active shift.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of <see cref="PhoneAssignmentDto"/> for the active shift.</returns>
    [HttpGet("active-shift")]
    public async Task<ActionResult<IEnumerable<PhoneAssignmentDto>>> GetCurrentPhoneAssignmentsForActiveShift(
        CancellationToken cancellationToken)
    {
        IEnumerable<PhoneAssignmentDto> result =
            await _phoneAssignmentService.GetCurrentPhoneAssignmentsForActiveShiftAsync(cancellationToken);

        return Ok(result);
    }

    #endregion
}
