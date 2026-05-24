// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Json;

using Core.DTOs;

namespace WebApi.Tests.Controllers.StaffAssignment;

/// <summary>
/// Integration tests for StaffAssignmentController.
/// </summary>
public class StaffAssignmentControllerTests(CustomWebApplicationFactory<Api.Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly HttpClient _client = factory.CreateAuthenticatedClient();

    [Fact]
    public async Task GetAssignments_ReturnsOk()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        HttpResponseMessage response = await _client.GetAsync(
            "/staff-assignments/list?shiftType=0&assignmentDate=2026-05-24", ct);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DeleteAssignment_UnknownId_ReturnsNotFound()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        HttpResponseMessage response = await _client.DeleteAsync($"/staff-assignments/{Guid.NewGuid()}", ct);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAssignment_UnknownId_ReturnsNotFound()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;
        StaffAssignmentDto dto = new()
        {
            ResidentId = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(),
            ShiftType = Domain.Enums.ShiftType.Night,
            AssignmentDate = DateTime.UtcNow
        };
        HttpResponseMessage response = await _client.PutAsJsonAsync($"/staff-assignments/{Guid.NewGuid()}", dto, ct);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
