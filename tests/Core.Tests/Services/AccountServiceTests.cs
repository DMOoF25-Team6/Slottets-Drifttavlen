// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.
using Core.DTOs.Identity;
using Core.Interfaces.Dto.Identity;
using Core.Interfaces.Managers;
using Core.Services;

using Moq;

namespace Core.Tests.Services;

public class AccountServiceTests
{
    private readonly Mock<IAccountManager> _AccountManagerMock;
    private readonly AccountService _service;

    public AccountServiceTests()
    {
        _AccountManagerMock = new Mock<IAccountManager>();
        _service = new AccountService(_AccountManagerMock.Object);
    }

    [Fact]
    public async Task CreateAccountAsync_ValidRequest_CallsManager()
    {
        // Arrange
        RegisterRequestDto request = new()
        {
            Email = "test@test.test",
            Password = "Password123!"
        };
        RegistrationResponseDto expectedResponse = new();
        _ = _AccountManagerMock
            .Setup(m => m.CreateAccountAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        RegistrationResponseDto result = await _service.CreateAccountAsync(request);

        // Assert
        Assert.Equal(expectedResponse, result);
        _AccountManagerMock.Verify(m => m.CreateAccountAsync(request), Times.Once);
    }

    [Fact]
    public async Task LogoutAsync_ValidRequest_CallsManager()
    {
        // Arrange
        LogoutRequestDto request = new() { RefreshToken = "test-jwt-token" };
        ILogoutResult expectedResponse = new LogoutResponseDto();
        _ = _AccountManagerMock
            .Setup(m => m.LogoutAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        ILogoutResult result = await _service.LogoutAsync(request);
        LogoutResponseDto logoutResponse = Assert.IsType<LogoutResponseDto>(result);

        // Assert
        _ = Assert.IsType<LogoutResponseDto>(logoutResponse);
        Assert.Equal(((LogoutResponseDto)expectedResponse).IsSuccessful, logoutResponse.IsSuccessful);
        _AccountManagerMock.Verify(m => m.LogoutAsync(request), Times.Once);
    }

    [Fact]
    public async Task RefreshTokenAsync_ValidRequest_CallsManager()
    {
        // Arrange
        RefreshTokenRequestDto request = new();
        RefreshTokenResponseDto expectedResponse = new();
        _ = _AccountManagerMock
            .Setup(m => m.RefreshTokenAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        RefreshTokenResponseDto result = await _service.RefreshTokenAsync(request);

        // Assert
        Assert.Equal(expectedResponse, result);
        _AccountManagerMock.Verify(m => m.RefreshTokenAsync(request), Times.Once);
    }

    [Theory]
    [InlineData("user1@test.test", "Password123!")]
    [InlineData("user2@test.test", "Password123!")]
    public async Task LoginAsync_ValidCredentials_CallsManager(string username, string password)
    {
        // Arrange
        LoginRequestDto request = new() { Email = username, Password = password };
        LoginResponseDto expectedResponse = new()
        {
            Token = "test-token",
            Email = username,
            ExpiresAt = DateTime.UtcNow.AddHours(1)
        };
        _ = _AccountManagerMock
            .Setup(m => m.LoginAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        ILoginResult result = await _service.LoginAsync(request);
        LoginResponseDto loginResponse = Assert.IsType<LoginResponseDto>(result);

        // Assert
        Assert.Equal(expectedResponse.Token, loginResponse.Token);
        Assert.Equal(expectedResponse.Email, loginResponse.Email);
        Assert.Equal(expectedResponse.ExpiresAt, loginResponse.ExpiresAt);
        _AccountManagerMock.Verify(m => m.LoginAsync(request), Times.Once);
    }
}
