// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs.Account;

namespace WebApi.Tests.Controllers.Accounts;

public class AccountControllerIntegrationTests(CustomWebApplicationFactory<Api.Program> factory) : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Register_ValidRequest_ReturnsOk()
    {
        // Arrange
        RegistrationRequestDto request = new() { EmailAddress = "test@example.com", Password = "Password123!", ConfirmPassword = "Password123!" };
        // Act
        HttpResponseMessage response = await _client.PostAsJsonAsync("/Account/register", request, cancellationToken: TestContext.Current.CancellationToken);
        // Assert
        _ = response.EnsureSuccessStatusCode();
        // Optionally assert response content
        Assert.Equal(200, (int)response.StatusCode);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsJwtAndRefreshToken()
    {
        // Arrange: Register user first
        RegistrationRequestDto registerRequest = new()
        {
            EmailAddress = "testlogin@example.com",
            Password = "Password123!",
            ConfirmPassword = "Password123!"
        };
        HttpResponseMessage registerResponse = await _client.PostAsJsonAsync("/Account/register", registerRequest, cancellationToken: TestContext.Current.CancellationToken);
        _ = registerResponse.EnsureSuccessStatusCode();

        LoginRequestDto loginRequest = new()
        {
            EmailAddress = "testlogin@example.com",
            Password = "Password123!"
        };
        // Act: Login
        HttpResponseMessage loginResponse = await _client.PostAsJsonAsync("/Account/login", loginRequest, cancellationToken: TestContext.Current.CancellationToken);
        _ = loginResponse.EnsureSuccessStatusCode();

        // Assert: JWT and refresh token in response
        LoginResponseDto? loginContent = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>(cancellationToken: TestContext.Current.CancellationToken);
        Assert.False(string.IsNullOrWhiteSpace(loginContent?.JwtToken));
        Assert.False(string.IsNullOrWhiteSpace(loginContent?.RefreshToken));
    }

    [Fact]
    public async Task Refresh_ValidRefreshToken_ReturnsNewJwt()
    {
        // Arrange: Register and login to get refresh token
        RegistrationRequestDto registerRequest = new()
        {
            EmailAddress = "testrefresh@example.com",
            Password = "Password123!",
            ConfirmPassword = "Password123!"
        };
        HttpResponseMessage registerResponse = await _client.PostAsJsonAsync("/Account/register", registerRequest, cancellationToken: TestContext.Current.CancellationToken);
        _ = registerResponse.EnsureSuccessStatusCode();

        LoginRequestDto loginRequest = new()
        {
            EmailAddress = "testrefresh@example.com",
            Password = "Password123!"
        };
        HttpResponseMessage loginResponse = await _client.PostAsJsonAsync("/Account/login", loginRequest, cancellationToken: TestContext.Current.CancellationToken);
        _ = loginResponse.EnsureSuccessStatusCode();

        LoginResponseDto? loginContent = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>(cancellationToken: TestContext.Current.CancellationToken);
        Assert.False(string.IsNullOrWhiteSpace(loginContent?.RefreshToken));

        // Act: Call refresh endpoint
        var refreshRequest = new
        {
            loginContent!.RefreshToken
        };
        HttpResponseMessage refreshResponse = await _client.PostAsJsonAsync("/Account/refresh", refreshRequest, cancellationToken: TestContext.Current.CancellationToken);
        _ = refreshResponse.EnsureSuccessStatusCode();

        // Assert: New JWT and refresh token
        LoginResponseDto? refreshContent = await refreshResponse.Content.ReadFromJsonAsync<LoginResponseDto>(cancellationToken: TestContext.Current.CancellationToken);
        Assert.False(string.IsNullOrWhiteSpace(refreshContent?.JwtToken));
        Assert.False(string.IsNullOrWhiteSpace(refreshContent?.RefreshToken));
        Assert.NotEqual(loginContent.JwtToken, refreshContent.JwtToken);
        Assert.NotEqual(loginContent.RefreshToken, refreshContent.RefreshToken);
    }

    [Fact]
    public async Task Logout_ReturnsOk()
    {
        // Arrange: Register a user first to ensure a valid authentication context
        RegistrationRequestDto request = new() { EmailAddress = "testlogout@example.com", Password = "Password123!", ConfirmPassword = "Password123!" };
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
