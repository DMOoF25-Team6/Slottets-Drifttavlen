// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;
using Domain.Enums;

namespace Core.Interfaces.Repositories;

public interface ITaskListRepository : IRepository<TaskList>
{
    Task<IEnumerable<TaskList>> GetDashboardTasksByDepartmentAsync(Department department, CancellationToken cancellationToken = default);
}
