// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

using Core.Interfaces.Repositories;

using Domain.Entities;



using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// API endpoints for employees.
/// </summary>

[ApiController]
[Route("employees")]
//[Authorize]
public class EmployeeController(
   IEmployeeRepository repository)
    : ControllerBase
{
    private readonly IEmployeeRepository _repository = repository;

    /// <summary>
    /// Retrieves all employees.
    /// </summary>

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), StatusCodes.Status200OK)]

    /// <summary>
    /// Gets all employees asynchronously.
    /// </summary>
    /// <param name="cancellationToken">
    /// Token used to cancel the request if needed.
    /// </param>
    /// <returns>
    /// HTTP 200 OK with a list of employees.
    /// </returns>
    public async Task<ActionResult<IEnumerable<EmployeeDto>>>GetAll(
        CancellationToken cancellationToken)
    {

        // Calls the manager layer to retrieve employees.
        IEnumerable<Employee> employees =
     await _repository.GetAllAsync(cancellationToken);

        IEnumerable<EmployeeDto> result = employees.Select(employee => new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Initials = employee.Initials,
            Department = employee.Department
        });

        return Ok(result);
    }

}
