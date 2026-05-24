// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;

using Core.DTOs;

using Domain.Enums;

using Infrastructure.Managers;

namespace Infrastructure.Tests.Managers;

public class TaskListManagerTests : HttpManagerTestBase
{
    private readonly TaskListManager _manager;

    public TaskListManagerTests() => _manager = new TaskListManager(FactoryMock.Object);

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetDashboardTasksByDepartmentAsync_DeserialisesPayload()
    {
        TaskListDto[] payload = [new(), new()];
        Setup("TaskList/dashboard/Slottet", Json(HttpStatusCode.OK, payload));

        IEnumerable<TaskListDto> result =
            await _manager.GetDashboardTasksByDepartmentAsync(Department.Slottet, CancellationToken.None);

        Assert.Equal(2, result.Count());
    }
}
