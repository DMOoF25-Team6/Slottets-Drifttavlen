// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

using Core.DTOs.Identity;

namespace WebApi.Tests.Controllers.Accounts;

public class AccountControllerTests(CustomWebApplicationFactory<Api.Program> factory) : IClassFixture<CustomWebApplicationFactory<Api.Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    #region Functionality Tests

    [Fact]
    [Trait("Category", "Functionality")]
    [Trait("Endpoint", "Delete")]
    public async Task DeleteUserAsync_ValidUser_ReturnsOkAndDeletesUser()
    {
        // Arrange: Login as admin to get JWT
        LoginResponseDto? adminLoginContent = await LoginAsAdminAsync();
        Assert.NotNull(adminLoginContent);

        // Arrange: CreateAccountAsync a new user to be deleted
        string uniqueEmail = await RegisterUserAsAdminAsync(adminLoginContent);

        GetUserIdByEmailRequestDto content = new() { Email = uniqueEmail };

        // Act: Find the user ID by email (using test helper)
        // If GetUserIdByEmailAsync returns a JSON string:
        string userIdJson = await GetUserIdByEmailAsync(content, _client, adminLoginContent.Token);
        string? userId = JsonDocument.Parse(userIdJson).RootElement.GetProperty("userId").GetString();
        Assert.NotNull(userId);

        // Act: Call delete endpoint as admin
        HttpRequestMessage deleteRequestMessage = new(HttpMethod.Delete, $"/Account/delete/{userId}");
        deleteRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminLoginContent.Token);
        // No Content for DELETE
        HttpResponseMessage deleteResponse = await _client.SendAsync(deleteRequestMessage, TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        DeleteUserResponseDto? deleteContent = await deleteResponse.Content.ReadFromJsonAsync<DeleteUserResponseDto>(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(deleteContent);
        Assert.True(deleteContent!.IsSuccessful);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    [Trait("Endpoint", "CreateAccountAsync")]
    public async Task Register_ValidRequest_ReturnsOk()
    {
        // Arrange: Login as admin to get JWT
        LoginResponseDto? adminLoginContent = await LoginAsAdminAsync();
        Assert.NotNull(adminLoginContent);

        // Arrange: CreateAccountAsync and login to get a valid refresh token
        _ = await RegisterUserAsAdminAsync(adminLoginContent);

        // Arrange: Use a different unique email for the test registration
        string testEmail = $"testlogin_{Guid.NewGuid()}@example.com";
        RegisterRequestDto request = new() { Email = testEmail, Password = "Password123!" };
        HttpRequestMessage registerRequestMessage = new(HttpMethod.Post, "/Account/register")
        {
            Content = JsonContent.Create(request)
        };
        registerRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminLoginContent.Token);
        // Act
        HttpResponseMessage response = await _client.SendAsync(registerRequestMessage, TestContext.Current.CancellationToken);
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
        // Arrange: Login as admin to get JWT
        LoginResponseDto? adminLoginContent = await LoginAsAdminAsync();
        Assert.NotNull(adminLoginContent);

        // Arrange: CreateAccountAsync and login to get a valid refresh token
        string uniqueEmail = await RegisterUserAsAdminAsync(adminLoginContent);

        // Act: Attempt to login
        LoginRequestDto loginRequest = new() { Email = uniqueEmail, Password = "Password123!" };
        HttpResponseMessage loginResponse = await _client.PostAsJsonAsync("/Account/login", loginRequest, cancellationToken: TestContext.Current.CancellationToken);

        LoginResponseDto? loginContent = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(loginContent);
        Assert.False(string.IsNullOrWhiteSpace(loginContent.RefreshToken));

        // Act: Call logout endpoint with refresh token header and body
        LogoutRequestDto logoutRequest = new() { RefreshToken = loginContent.RefreshToken };
        HttpRequestMessage requestMessage = new(HttpMethod.Post, "/Account/logout")
        {
            Content = JsonContent.Create(logoutRequest)
        };
        requestMessage.Headers.Add("X-Refresh-Token", loginContent.RefreshToken!);
        HttpResponseMessage response = await _client.SendAsync(requestMessage, TestContext.Current.CancellationToken);

        // Assert
        _ = response.EnsureSuccessStatusCode();
    }

    // Additional tests for valid/expired tokens and user not found can be added with proper setup/mocking.
    [Fact]
    [Trait("Category", "Functionality")]
    [Trait("Endpoint", "Login")]
    public async Task Login_ValidCredentials_ReturnsOk()
    {
        // Arrange: Login as admin to get JWT
        LoginResponseDto? adminLoginContent = await LoginAsAdminAsync();
        Assert.NotNull(adminLoginContent);

        // Arrange: CreateAccountAsync and login to get a valid refresh token
        string uniqueEmail = await RegisterUserAsAdminAsync(adminLoginContent);

        // Act: Attempt to login
        LoginRequestDto loginRequest = new() { Email = uniqueEmail, Password = "Password123!" };
        HttpResponseMessage loginResponse = await _client.PostAsJsonAsync("/Account/login", loginRequest, cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        _ = loginResponse.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
    }

    private async Task<HttpResponseMessage> LoginUserAsync(string uniqueEmail)
    {
        LoginRequestDto loginRequest = new() { Email = uniqueEmail, Password = "Password123!" };
        HttpResponseMessage loginResponse = await _client.PostAsJsonAsync("/Account/login", loginRequest, cancellationToken: TestContext.Current.CancellationToken);
        _ = loginResponse.EnsureSuccessStatusCode();
        return loginResponse;
    }

    [Fact]
    [Trait("Category", "Functionality")]
    [Trait("Endpoint", "Logout")]
    public async Task Logout_RevokesRefreshToken_CannotRefreshAfterLogout()
    {
        // Arrange: Login as admin to get JWT
        LoginResponseDto? adminLoginContent = await LoginAsAdminAsync();
        Assert.NotNull(adminLoginContent);

        // Arrange: CreateAccountAsync and login to get a valid refresh token
        string uniqueEmail = await RegisterUserAsAdminAsync(adminLoginContent);

        // Arrange: Login to get refresh token
        _ = await LoginUserAsync(uniqueEmail);

        // Login to get refresh token
        LoginRequestDto login = new() { Email = uniqueEmail, Password = "Password123!" };

        HttpResponseMessage loginResp = await _client.PostAsJsonAsync("/Account/login", login, cancellationToken: TestContext.Current.CancellationToken);
        _ = loginResp.EnsureSuccessStatusCode();
        LoginResponseDto? loginContent = await loginResp.Content.ReadFromJsonAsync<LoginResponseDto>(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(loginContent);
        string? refreshToken = loginContent.RefreshToken;
        Assert.NotNull(refreshToken);
        // Logout with refresh token in header
        HttpRequestMessage logoutReq = new(HttpMethod.Post, "/Account/logout");
        logoutReq.Headers.Add("X-Refresh-Token", refreshToken);
        LogoutRequestDto logoutDto = new() { RefreshToken = refreshToken };
        logoutReq.Content = JsonContent.Create(logoutDto);
        HttpResponseMessage logoutResp = await _client.SendAsync(logoutReq, TestContext.Current.CancellationToken);
        _ = logoutResp.EnsureSuccessStatusCode();
        // Try to refresh with the same token (should fail)
        RefreshTokenRequestDto refreshReq = new() { RefreshToken = refreshToken! };
        HttpResponseMessage refreshResp = await _client.PostAsJsonAsync("/Account/refresh", refreshReq, cancellationToken: TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.Unauthorized, refreshResp.StatusCode);
    }

    #endregion

    #region Concurrency
    //[Fact]
    //[Trait("Category", "Concurrency")]
    //[Trait("Endpoint", "Register")]
    //public async Task Register_ConcurrentSameEmail_OneSucceedsOneFails()
    //{
    //    // Arrange
    //    LoginResponseDto? adminLoginContent = await LoginAsAdminAsync();
    //    Assert.NotNull(adminLoginContent);
    //    string email = $"concurrent_{Guid.NewGuid()}@example.com";
    //    string password = "Password123!";
    //    RegisterRequestDto registerRequest = new() { Email = email, Password = password };
    //    HttpRequestMessage request1 = new(HttpMethod.Post, "/Account/register") { Content = JsonContent.Create(registerRequest) };
    //    HttpRequestMessage request2 = new(HttpMethod.Post, "/Account/register") { Content = JsonContent.Create(registerRequest) };
    //    request1.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminLoginContent.Token);
    //    request2.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminLoginContent.Token);
    //    // Act
    //    Task<HttpResponseMessage> task1 = _client.SendAsync(request1, TestContext.Current.CancellationToken);
    //    Task<HttpResponseMessage> task2 = _client.SendAsync(request2, TestContext.Current.CancellationToken);
    //    HttpResponseMessage[] results = await Task.WhenAll(task1, task2);
    //    // Assert
    //    Assert.Contains(results, r => r.StatusCode == HttpStatusCode.OK);
    //    Assert.Contains(results, r => r.StatusCode == HttpStatusCode.BadRequest);
    //}

    [Fact]
    [Trait("Category", "Concurrency")]
    [Trait("Endpoint", "Login")]
    public async Task Login_ConcurrentSameCredentials_AllReturnOk()
    {
        // Arrange
        LoginResponseDto? adminLoginContent = await LoginAsAdminAsync();
        Assert.NotNull(adminLoginContent);
        string email = $"concurrentlogin_{Guid.NewGuid()}@example.com";
        string password = "Password123!";
        _ = await RegisterUserAsAdminAsync(adminLoginContent, email, password);
        LoginRequestDto loginRequest = new() { Email = email, Password = password };
        // Act
        Task<HttpResponseMessage>[] tasks = [.. Enumerable.Range(0, 5).Select(_ => _client.PostAsJsonAsync("/Account/login", loginRequest, cancellationToken: TestContext.Current.CancellationToken))];
        HttpResponseMessage[] results = await Task.WhenAll(tasks);
        // Assert
        Assert.All(results, r => Assert.Equal(HttpStatusCode.OK, r.StatusCode));
    }
    #endregion

    #region EdgeCase
    [Fact]
    [Trait("Category", "EdgeCase")]
    [Trait("Endpoint", "Register")]
    public async Task Register_DuplicateEmail_ReturnsBadRequest()
    {
        // Arrange
        LoginResponseDto? adminLoginContent = await LoginAsAdminAsync();
        Assert.NotNull(adminLoginContent);
        string email = $"duplicate_{Guid.NewGuid()}@example.com";
        string password = "Password123!";
        _ = await RegisterUserAsAdminAsync(adminLoginContent, email, password);
        // Act
        HttpResponseMessage response = await RegisterUserAsAdminAsync(adminLoginContent, email, password);
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    [Trait("Endpoint", "Delete")]
    public async Task DeleteUserAsync_InvalidUserIdFormat_ReturnsBadRequest()
    {
        // Arrange
        LoginResponseDto? adminLoginContent = await LoginAsAdminAsync();
        Assert.NotNull(adminLoginContent);
        string invalidUserId = "not-a-guid";
        HttpRequestMessage deleteRequest = new(HttpMethod.Delete, $"/Account/delete/{invalidUserId}");
        deleteRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminLoginContent.Token);
        // Act
        HttpResponseMessage response = await _client.SendAsync(deleteRequest, TestContext.Current.CancellationToken);
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    [Trait("Endpoint", "Logout")]
    public async Task Logout_MissingRefreshToken_ReturnsBadRequest()
    {
        // Arrange
        LoginResponseDto? adminLoginContent = await LoginAsAdminAsync();
        Assert.NotNull(adminLoginContent);
        LogoutRequestDto logoutRequest = new() { RefreshToken = string.Empty };
        HttpRequestMessage requestMessage = new(HttpMethod.Post, "/Account/logout") { Content = JsonContent.Create(logoutRequest) };
        // Act
        HttpResponseMessage response = await _client.SendAsync(requestMessage, TestContext.Current.CancellationToken);
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    [Trait("Endpoint", "Refresh")]
    public async Task Refresh_RevokedToken_ReturnsUnauthorized()
    {
        // Arrange
        LoginResponseDto? adminLoginContent = await LoginAsAdminAsync();
        Assert.NotNull(adminLoginContent);
        string email = $"revoked_{Guid.NewGuid()}@example.com";
        string password = "Password123!";
        _ = await RegisterUserAsAdminAsync(adminLoginContent, email, password);
        LoginRequestDto loginRequest = new() { Email = email, Password = password };
        HttpResponseMessage loginResponse = await _client.PostAsJsonAsync("/Account/login", loginRequest, cancellationToken: TestContext.Current.CancellationToken);
        LoginResponseDto? loginContent = await loginResponse.Content.ReadFromJsonAsync<LoginResponseDto>(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(loginContent);
        string? refreshToken = loginContent.RefreshToken;
        Assert.NotNull(refreshToken);
        LogoutRequestDto logoutRequest = new() { RefreshToken = refreshToken };
        HttpRequestMessage logoutMessage = new(HttpMethod.Post, "/Account/logout") { Content = JsonContent.Create(logoutRequest) };
        logoutMessage.Headers.Add("X-Refresh-Token", refreshToken!);
        HttpResponseMessage logoutResponse = await _client.SendAsync(logoutMessage, TestContext.Current.CancellationToken);
        _ = logoutResponse.EnsureSuccessStatusCode();
        // Act
        RefreshTokenRequestDto refreshRequest = new() { RefreshToken = refreshToken! };
        HttpResponseMessage refreshResponse = await _client.PostAsJsonAsync("/Account/refresh", refreshRequest, cancellationToken: TestContext.Current.CancellationToken);
        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, refreshResponse.StatusCode);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    [Trait("Endpoint", "Refresh")]
    public async Task Refresh_UserNotFound_ReturnsUnauthorized()
    {
        // Arrange
        string fakeToken = "nonexistenttoken";
        RefreshTokenRequestDto refreshRequest = new() { RefreshToken = fakeToken };
        // Act
        HttpResponseMessage response = await _client.PostAsJsonAsync("/Account/refresh", refreshRequest, cancellationToken: TestContext.Current.CancellationToken);
        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Theory]
    [Trait("Category", "EdgeCase")]
    [Trait("Endpoint", "GetUserIdByEmail")]
    [InlineData("admin@example.com", true)]
    [InlineData("notfound@example.com", false)]
    public async Task GetUserIdByEmailAsync_VariousCases_ReturnsExpected(string email, bool shouldExist)
    {
        // Arrange
        LoginResponseDto? adminLoginContent = await LoginAsAdminAsync();
        Assert.NotNull(adminLoginContent);
        GetUserIdByEmailRequestDto content = new() { Email = email };
        HttpRequestMessage request = new(HttpMethod.Post, "/Account/get-id-by-email") { Content = JsonContent.Create(content) };
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminLoginContent.Token);
        // Act
        HttpResponseMessage response = await _client.SendAsync(request, TestContext.Current.CancellationToken);
        // Assert
        if (shouldExist)
        {
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        else
        {
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
    #endregion

    #region Helper Methods
    private async Task<LoginResponseDto?> LoginAsAdminAsync()
    {
        // Arrange: Login as admin to get JWT
        LoginRequestDto adminLogin = new() { Email = "admin@example.com", Password = "Password123!" };
        HttpResponseMessage adminLoginResponse = await _client.PostAsJsonAsync("/Account/login", adminLogin, cancellationToken: TestContext.Current.CancellationToken);
        _ = adminLoginResponse.EnsureSuccessStatusCode();
        LoginResponseDto? adminLoginContent = await adminLoginResponse.Content.ReadFromJsonAsync<LoginResponseDto>(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(adminLoginContent);
        Assert.False(string.IsNullOrWhiteSpace(adminLoginContent.Token));
        return adminLoginContent;
    }

    private async Task<string> RegisterUserAsAdminAsync(LoginResponseDto adminLoginContent)
    {
        string uniqueEmail = $"testlogin_{Guid.NewGuid()}@example.com";
        RegisterRequestDto registerRequest = new() { Email = uniqueEmail, Password = "Password123!" };
        HttpRequestMessage registerRequestMessage = new(HttpMethod.Post, "/Account/register")
        {
            Content = JsonContent.Create(registerRequest)
        };
        registerRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminLoginContent.Token);
        HttpResponseMessage registerResponse = await _client.SendAsync(registerRequestMessage, TestContext.Current.CancellationToken);
        _ = registerResponse.EnsureSuccessStatusCode();
        return uniqueEmail;
    }

    private async Task<HttpResponseMessage> RegisterUserAsAdminAsync(LoginResponseDto adminLoginContent, string email, string password)
    {
        RegisterRequestDto registerRequest = new() { Email = email, Password = password };
        HttpRequestMessage registerRequestMessage = new(HttpMethod.Post, "/Account/register")
        {
            Content = JsonContent.Create(registerRequest)
        };
        registerRequestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminLoginContent.Token);
        HttpResponseMessage registerResponse = await _client.SendAsync(registerRequestMessage, TestContext.Current.CancellationToken);
        return registerResponse;
    }

    public static async Task<string> GetUserIdByEmailAsync(GetUserIdByEmailRequestDto content, HttpClient client, string adminToken)
    {
        // Use POST to avoid sending email in clear text in the URL.
        HttpRequestMessage request = new(HttpMethod.Post, "/Account/get-id-by-email")
        {
            Content = JsonContent.Create(content)
        };
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminToken);
        HttpResponseMessage response = await client.SendAsync(request);
        _ = response.EnsureSuccessStatusCode();
        string userId = await response.Content.ReadAsStringAsync();
        return userId.Trim('"'); // Remove quotes if returned as JSON string
    }
    #endregion
}
