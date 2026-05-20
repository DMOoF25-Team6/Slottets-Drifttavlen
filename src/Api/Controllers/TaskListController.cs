// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;
using Core.Mappers;

using Domain.Entities;
using Domain.Enums;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskListController(ITaskListRepository taskListRepository) : ControllerBase
{
    private readonly ITaskListRepository _taskListRepository = taskListRepository;

    [HttpGet("dashboard/{department}")]
    public async Task<IActionResult> GetDashboardTasksByDepartment(Department department, CancellationToken cancellationToken = default)
    {
        IEnumerable<TaskList> tasks = await _taskListRepository.GetDashboardTasksByDepartmentAsync(department, cancellationToken);
        return Ok(tasks.Select(t => t.ToTaskListDto()));
    }
}
