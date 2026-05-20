// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

using Microsoft.AspNetCore.Components;

namespace WebUI.Client.Components.TaskList;

public partial class TaskList
{
    [Parameter]
    public IEnumerable<TaskListDto> Tasks { get; set; } = [];

}