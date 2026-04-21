// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs;

namespace WebApi.Tests.Controllers.PhoneAssignments;

public class PhoneAssignmentsControllerIntegrationTests(CustomWebApplicationFactory<Api.Program> factory) : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task GetCurrentPhoneAssignmentsForActiveShift_ReturnsOk()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync(
            "/PhoneAssignments/active-shift",
            TestContext.Current.CancellationToken);

        // Assert
        _ = response.EnsureSuccessStatusCode();
        Assert.Equal(200, (int)response.StatusCode);
    }

    [Fact]
    public async Task GetCurrentPhoneAssignmentsForActiveShift_ReturnsPhoneAssignmentDtoList()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync(
            "/PhoneAssignments/active-shift",
            TestContext.Current.CancellationToken);

        // Assert
        _ = response.EnsureSuccessStatusCode();
        IEnumerable<PhoneAssignmentDto>? result = await response.Content
            .ReadFromJsonAsync<IEnumerable<PhoneAssignmentDto>>(
                cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
    }
}
