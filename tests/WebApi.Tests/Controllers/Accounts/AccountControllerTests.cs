// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Json;

using Core.DTOs.Identity;

namespace WebApi.Tests.Controllers.Accounts;

public class AccountControllerTests : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly HttpClient _client;

    public AccountControllerTests(CustomWebApplicationFactory<Api.Program> factory)
    {
        _client = factory.CreateClient();
    }

    #region Functionality Tests

    [Fact]
    [Trait("Category", "Functionality")]
    [Trait("Endpoint", "Register")]
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
    [Trait("Category", "Functionality")]
    [Trait("Endpoint", "Logout")]
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

    // Additional tests for valid/expired tokens and user not found can be added with proper setup/mocking.
    [Fact]
    [Trait("Category", "Functionality")]
    [Trait("Endpoint", "Login")]
    public async Task Login_ValidCredentials_ReturnsOk()
    {
        // Arrange: Register a user first
        string uniqueEmail = $"testlogin_{Guid.NewGuid()}@example.com";
        RegisterRequestDto registerRequest = new() { Email = uniqueEmail, Password = "Password123!" };
        HttpResponseMessage registerResponse = await _client.PostAsJsonAsync("/Account/register", registerRequest, cancellationToken: TestContext.Current.CancellationToken);
        _ = registerResponse.EnsureSuccessStatusCode();

        // Act: Attempt to login
        LoginRequestDto loginRequest = new() { Email = uniqueEmail, Password = "Password123!" };
        HttpResponseMessage loginResponse = await _client.PostAsJsonAsync("/Account/login", loginRequest, cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        _ = loginResponse.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
    }
    [Fact]
    [Trait("Category", "Functionality")]
    [Trait("Endpoint", "Refresh")]
    public async Task Refresh_ValidToken_ReturnsOkAndNewTokens()
    {
        // Arrange: Register and login to get a valid refresh token
        string uniqueEmail = $"testrefresh_{Guid.NewGuid()}@example.com";
        RegisterRequestDto registerRequest = new() { Email = uniqueEmail, Password = "Password123!" };
        HttpResponseMessage registerResponse = await _client.PostAsJsonAsync("/Account/register", registerRequest, cancellationToken: TestContext.Current.CancellationToken);
        _ = registerResponse.EnsureSuccessStatusCode();

        LoginRequestDto loginRequest = new() { Email = uniqueEmail, Password = "Password123!" };
        HttpResponseMessage loginResponse = await _client.PostAsJsonAsync("/Account/login", loginRequest, cancellationToken: TestContext.Current.CancellationToken);
        _ = loginResponse.EnsureSuccessStatusCode();

        // Extract refresh token from login response
        LoginResponseDto? loginContent = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(loginContent);
        Assert.False(string.IsNullOrWhiteSpace(loginContent.RefreshToken));

        RefreshTokenRequestDto refreshRequest = new() { RefreshToken = loginContent.RefreshToken! };

        // Act
        HttpResponseMessage refreshResponse = await _client.PostAsJsonAsync("/Account/refresh", refreshRequest, cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        _ = refreshResponse.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, refreshResponse.StatusCode);
        LoginResponseDto? refreshContent = await refreshResponse.Content.ReadFromJsonAsync<LoginResponseDto>(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(refreshContent);
        Assert.False(string.IsNullOrWhiteSpace(refreshContent.JwtToken));
        Assert.False(string.IsNullOrWhiteSpace(refreshContent.RefreshToken));
    }

    #endregion

    #region Edge Cases

    [Theory]
    [Trait("Category", "EdgeCase")]
    [Trait("Endpoint", "Login")]
    [InlineData("notregistered@example.com", "Password123!", HttpStatusCode.Unauthorized)] // Unregistered user
    [InlineData("testlogin@example.com", "WrongPassword", HttpStatusCode.Unauthorized)] // Wrong password
    [InlineData("", "Password123!", HttpStatusCode.BadRequest)] // Empty email
    [InlineData("testlogin@example.com", "", HttpStatusCode.BadRequest)] // Empty password
    public async Task Login_EdgeCases_ReturnsExpectedStatus(string email, string password, HttpStatusCode expectedStatus)
    {
        // Arrange: Register a user for the wrong password/empty password/email cases
        string testEmail = email;
        if (email == "testlogin@example.com")
        {
            testEmail = $"testlogin_{Guid.NewGuid()}@example.com";
            RegisterRequestDto registerRequest = new() { Email = testEmail, Password = "Password123!" };
            HttpResponseMessage registerResponse = await _client.PostAsJsonAsync("/Account/register", registerRequest, cancellationToken: TestContext.Current.CancellationToken);
            _ = registerResponse.EnsureSuccessStatusCode();
        }

        // Act
        LoginRequestDto loginRequest = new() { Email = testEmail, Password = password };
        HttpResponseMessage loginResponse = await _client.PostAsJsonAsync("/Account/login", loginRequest, cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(expectedStatus, loginResponse.StatusCode);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    [Trait("Endpoint", "Refresh")]
    public async Task Refresh_InvalidModel_ReturnsBadRequest()
    {
        // Arrange: Send empty request
        RefreshTokenRequestDto request = new() { RefreshToken = null! };
        // Act
        HttpResponseMessage response = await _client.PostAsJsonAsync("/Account/refresh", request, cancellationToken: TestContext.Current.CancellationToken);
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    [Trait("Endpoint", "Refresh")]
    public async Task Refresh_InvalidToken_ReturnsUnauthorized()
    {
        // Arrange: Use a random invalid token
        RefreshTokenRequestDto request = new() { RefreshToken = "invalidtoken" };
        // Act
        HttpResponseMessage response = await _client.PostAsJsonAsync("/Account/refresh", request, cancellationToken: TestContext.Current.CancellationToken);
        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Theory]
    [Trait("Category", "EdgeCase")]
    [Trait("Endpoint", "Register")]
    [InlineData("", "Password123!", HttpStatusCode.BadRequest)] // Empty email
    [InlineData("test2@example.com", "", HttpStatusCode.BadRequest)] // Empty password
    [InlineData("not-an-email", "Password123!", HttpStatusCode.BadRequest)] // Invalid email format
    [InlineData("test3@example.com", "short", HttpStatusCode.BadRequest)] // Too short password
    public async Task Register_EdgeCases_ReturnsExpectedStatus(string email, string password, HttpStatusCode expectedStatus)
    {
        // Arrange
        RegisterRequestDto request = new() { Email = email, Password = password };
        // Act
        HttpResponseMessage response = await _client.PostAsJsonAsync("/Account/register", request, cancellationToken: TestContext.Current.CancellationToken);
        // Assert
        Assert.Equal(expectedStatus, response.StatusCode);
    }
    #endregion
}
