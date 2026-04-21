// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs.Identity;

namespace WebApi.Tests.Controllers.Accounts;

public class AccountControllerIntegrationTests(CustomWebApplicationFactory<Api.Program> factory) : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Register_ValidRequest_ReturnsOk()
    {
        // Arrange
        RegisterRequestDto request = new() { Email = "test@example.com", Password = "Password123!" };
        // Act
        HttpResponseMessage response = await _client.PostAsJsonAsync("/Account/register", request, cancellationToken: TestContext.Current.CancellationToken);
        // Assert
        _ = response.EnsureSuccessStatusCode();
        // Optionally assert response content
        Assert.Equal(200, (int)response.StatusCode);
    }


    [Fact]
    public async Task Logout_ReturnsOk()
    {
        // Arrange: Register a user first to ensure a valid authentication context
        RegisterRequestDto request = new() { Email = "testlogout@example.com", Password = "Password123!" };
        HttpResponseMessage registerResponse = await _client.PostAsJsonAsync("/Account/register", request, cancellationToken: TestContext.Current.CancellationToken);
        _ = registerResponse.EnsureSuccessStatusCode();

        // Act: Login to get authentication cookie or token if required by your API
        // (If your API uses cookies, you may need to handle authentication here)

        // Act: Call logout endpoint
        HttpResponseMessage response = await _client.PostAsync("/Account/logout", null, TestContext.Current.CancellationToken);

        // Assert
        _ = response.EnsureSuccessStatusCode();
    }
}