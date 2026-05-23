// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Json;

using Core.DTOs;

namespace WebApi.Tests.Controllers.Employees;

/// <summary>
/// Integration tests for EmployeeController.
/// </summary>
public class EmployeeControllerTests(CustomWebApplicationFactory<Api.Program> factory)
    : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly HttpClient _client = factory.CreateAuthenticatedClient();

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        HttpResponseMessage response = await _client.GetAsync("/employees", ct);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ReturnsEmployeeList()
    {
        CancellationToken ct = TestContext.Current.CancellationToken;

        IEnumerable<EmployeeDto>? result =
            await _client.GetFromJsonAsync<IEnumerable<EmployeeDto>>("/employees", ct);

        Assert.NotNull(result);
    }
}
