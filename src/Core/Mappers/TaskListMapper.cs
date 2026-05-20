// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.DTOs;

using Domain.Entities;

namespace Core.Mappers;

public static class TaskListMapper
{
    public static TaskListDto ToTaskListDto (this TaskList taskList)
    {
        return new TaskListDto
        {
            Id = taskList.Id,
            Title = taskList.Title,
            Description = taskList.Description,
            TaskListStatus = taskList.TaskStatus,
            DueTime = taskList.DueTime,
            Department = taskList.Department
        };
    }
}
