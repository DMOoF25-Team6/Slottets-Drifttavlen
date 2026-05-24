// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;

namespace WebApi.Tests.Controllers.TaskList;

/// <summary>
/// Integration tests for TaskListController (UC-002 dashboard task list).
/// </summary>
public class TaskListControllerTests(CustomWebApplicationFactory<Api.Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly HttpClient _client = factory.CreateAuthenticatedClient();

    [Fact]
    public async Task GetDashboardTasksByDepartment_ReturnsOk()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        HttpResponseMessage response = await _client.GetAsync("/TaskList/dashboard/Slottet", ct);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
