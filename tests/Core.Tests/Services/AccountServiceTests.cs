// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.
using Core.DTOs.Identity;
using Core.Interfaces.Managers;
using Core.Services;

using Moq;

namespace Core.Tests.Services;

public class AccountServiceTests
{
    private readonly Mock<IAccountManager> _accountManagerMock;
    private readonly AccountService _service;

    public AccountServiceTests()
    {
        _accountManagerMock = new Mock<IAccountManager>();
        _service = new AccountService(_accountManagerMock.Object);
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
        _ = _accountManagerMock
            .Setup(m => m.CreateAccountAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        RegistrationResponseDto result = await _service.CreateAccountAsync(request);

        // Assert
        Assert.Equal(expectedResponse, result);
        _accountManagerMock.Verify(m => m.CreateAccountAsync(request), Times.Once);
    }

    [Fact]
    public async Task LogoutAsync_ValidRequest_CallsManager()
    {
        // Arrange
        LogoutRequestDto request = new() { JwtToken = "test-jwt-token" };
        LogoutResponseDto expectedResponse = new();
        _ = _accountManagerMock
            .Setup(m => m.LogoutAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        LogoutResponseDto result = await _service.LogoutAsync(request);

        // Assert
        Assert.Equal(expectedResponse, result);
        _accountManagerMock.Verify(m => m.LogoutAsync(request), Times.Once);
    }

    [Fact]
    public async Task RefreshTokenAsync_ValidRequest_CallsManager()
    {
        // Arrange
        RefreshTokenRequestDto request = new();
        RefreshTokenResponseDto expectedResponse = new();
        _ = _accountManagerMock
            .Setup(m => m.RefreshTokenAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        RefreshTokenResponseDto result = await _service.RefreshTokenAsync(request);

        // Assert
        Assert.Equal(expectedResponse, result);
        _accountManagerMock.Verify(m => m.RefreshTokenAsync(request), Times.Once);
    }

    [Theory]
    [InlineData("user1@test.test", "Password123!")]
    [InlineData("user2@test.test", "Password123!")]
    public async Task LoginAsync_ValidCredentials_CallsManager(string username, string password)
    {
        // Arrange
        LoginRequestDto request = new() { Email = username, Password = password };
        LoginResponseDto expectedResponse = new();
        _ = _accountManagerMock
            .Setup(m => m.LoginAsync(request))
            .ReturnsAsync(expectedResponse);

        // Act
        LoginResponseDto result = await _service.LoginAsync(request);

        // Assert
        Assert.Equal(expectedResponse, result);
        _accountManagerMock.Verify(m => m.LoginAsync(request), Times.Once);
    }
}
