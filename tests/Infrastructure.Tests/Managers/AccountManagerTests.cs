// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Json;

using Core.DTOs.Identity;
using Core.Interfaces.Dto.Identity;

using Infrastructure.Managers;

using Moq;
using Moq.Protected;

namespace Infrastructure.Tests.Managers;

public class AccountManagerTests
{
    private static HttpClient CreateMockHttpClient(HttpResponseMessage response)
    {
        Mock<HttpMessageHandler> handlerMock = new(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response)
            .Verifiable();
        return new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://localhost")
        };
    }

    private class FakeHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpClient _client;
        public FakeHttpClientFactory(HttpClient client) => _client = client;
        public HttpClient CreateClient(string name) => _client;
    }

    [Fact]
    public async Task Register_ValidResponse_ReturnsSuccess()
    {
        // Arrange
        RegistrationResponseDto expected = new() { IsSuccessful = true };
        HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(expected)
        };
        HttpClient client = CreateMockHttpClient(response);
        FakeHttpClientFactory factory = new(client);
        AccountManager manager = new(factory);
        RegisterRequestDto request = new()
        {
            Email = "test@example.com",
            Password = "TestPassword123!"
        };

        // Act
        RegistrationResponseDto result = await manager.CreateAccountAsync(request);

        // Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public async Task Register_InvalidJson_ReturnsFailedResponse()
    {
        // Arrange
        HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = new StringContent("not json")
        };
        HttpClient client = CreateMockHttpClient(response);
        FakeHttpClientFactory factory = new(client);
        AccountManager manager = new(factory);
        RegisterRequestDto request = new()
        {
            Email = "test@example.com",
            Password = "TestPassword123!"
        };

        // Act
        RegistrationResponseDto result = await manager.CreateAccountAsync(request);

        // Assert
        Assert.False(result.IsSuccessful);
        Assert.Contains("Failed to parse registration response.", result.ErrorMessages);
    }

    [Fact]
    public async Task LoginAsync_ValidResponse_ReturnsLoginResponseDto()
    {
        // Arrange
        LoginResponseDto expected = new()
        {
            Token = "abc",
            Email = "test@example.com",
            ExpiresAt = DateTime.UtcNow.AddHours(1)
        };
        HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(expected)
        };
        HttpClient client = CreateMockHttpClient(response);
        FakeHttpClientFactory factory = new(client);
        AccountManager manager = new(factory);
        LoginRequestDto request = new()
        {
            Email = "test@example.com",
            Password = "TestPassword123!"
        };

        // Act
        ILoginResult result = await manager.LoginAsync(request);

        // Assert
        _ = Assert.IsType<LoginResponseDto>(result);
        Assert.Equal("abc", ((LoginResponseDto)result).Token);
    }

    [Fact]
    public async Task LoginAsync_InvalidJson_ReturnsErrorDto()
    {
        // Arrange
        HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = new StringContent("not json")
        };
        HttpClient client = CreateMockHttpClient(response);
        FakeHttpClientFactory factory = new(client);
        AccountManager manager = new(factory);
        LoginRequestDto request = new()
        {
            Email = "test@example.com",
            Password = "TestPassword123!"
        };

        // Act
        ILoginResult result = await manager.LoginAsync(request);

        // Assert
        _ = Assert.IsType<ErrorDto>(result);
        Assert.Contains("Failed to parse login response.", ((ErrorDto)result).ErrorMessages!);
    }

    [Fact]
    public async Task LogoutAsync_ValidResponse_ReturnsLogoutResponseDto()
    {
        // Arrange
        LogoutResponseDto expected = new();
        HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(expected)
        };
        HttpClient client = CreateMockHttpClient(response);
        FakeHttpClientFactory factory = new(client);
        AccountManager manager = new(factory);
        LogoutRequestDto request = new()
        {
            RefreshToken = "test-refresh-token"
        };

        // Act
        ILogoutResult result = await manager.LogoutAsync(request);

        // Assert
        _ = Assert.IsType<LogoutResponseDto>(result);
    }

    [Fact]
    public async Task LogoutAsync_InvalidJson_ReturnsErrorDto()
    {
        // Arrange
        HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = new StringContent("not json")
        };
        HttpClient client = CreateMockHttpClient(response);
        FakeHttpClientFactory factory = new(client);
        AccountManager manager = new(factory);
        LogoutRequestDto request = new()
        {
            RefreshToken = "test-refresh-token"
        };

        // Act
        ILogoutResult result = await manager.LogoutAsync(request);

        // Assert
        _ = Assert.IsType<ErrorDto>(result);
        Assert.Contains("Failed to parse logout response.", ((ErrorDto)result).ErrorMessages!);
    }

    [Fact]
    public async Task RefreshTokenAsync_ValidResponse_ReturnsRefreshTokenResponseDto()
    {
        // Arrange
        RefreshTokenResponseDto expected = new() { JwtToken = "jwt", RefreshToken = "refresh" };
        HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(expected)
        };
        HttpClient client = CreateMockHttpClient(response);
        FakeHttpClientFactory factory = new(client);
        AccountManager manager = new(factory);
        RefreshTokenRequestDto request = new();

        // Act
        RefreshTokenResponseDto result = await manager.RefreshTokenAsync(request);

        // Assert
        Assert.Equal("jwt", result.JwtToken);
        Assert.Equal("refresh", result.RefreshToken);
    }

    [Fact]
    public async Task RefreshTokenAsync_InvalidJson_ReturnsFailedResponse()
    {
        // Arrange
        HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = new StringContent("not json")
        };
        HttpClient client = CreateMockHttpClient(response);
        FakeHttpClientFactory factory = new(client);
        AccountManager manager = new(factory);
        RefreshTokenRequestDto request = new();

        // Act
        RefreshTokenResponseDto result = await manager.RefreshTokenAsync(request);

        // Assert
        Assert.Null(result.JwtToken);
        Assert.Null(result.RefreshToken);
        Assert.Contains("Failed to parse refresh token response.", result.ErrorMessages!);
    }
}
