// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.DTOs;

using Domain.Entities;
using Domain.Enums;

namespace Core.Interfaces.Services;
public interface ITaskListService
{
    Task<IEnumerable<TaskListDto>> GetAvailableTasksByDepartmentAsync(Department department, CancellationToken cancellationToken = default);
}
