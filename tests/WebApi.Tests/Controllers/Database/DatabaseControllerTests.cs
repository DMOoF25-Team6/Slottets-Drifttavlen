// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Json;
using Xunit;
using WebApi.Tests;

namespace WebApi.Tests.Controllers.Database;

public class DatabaseControllerTests(CustomWebApplicationFactory<Api.Program> factory) : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    [Trait("Category", "Functionality")]
    [Trait("Endpoint", "IsConnected")]
    public async Task IsConnected_ReturnsOkAndBoolean()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/Database/isconnected", TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        bool? isConnected = await response.Content.ReadFromJsonAsync<bool>(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(isConnected);
        // No assertion on value, as it depends on test DB setup
    }
}
