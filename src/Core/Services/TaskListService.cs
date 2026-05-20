// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.DTOs;
using Core.Interfaces.Managers;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Mappers;

using Domain.Entities;
using Domain.Enums;

namespace Core.Services;

public class TaskListService(ITaskListManager taskListManager) : ITaskListService
{
    private readonly ITaskListManager _taskListManager = taskListManager;


    public async Task<IEnumerable<TaskListDto>> GetAvailableTasksByDepartmentAsync(Department department, CancellationToken cancellationToken = default)
    {
        return await _taskListManager.GetDashboardTasksByDepartmentAsync(department, cancellationToken);
    }
}
