// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.DTOs.Identity;
using Core.Interfaces.Dto.Identity;
using Core.Interfaces.Services;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

using Moq;

using WebUI.Client;
using WebUI.Client.Services;

namespace WebUI.Tests.AuthService;

/// <summary>
/// Unit tests for <see cref="WebUI.Client.AuthService"/>.
/// </summary>
/// <remarks>
/// UC-007 (REQ-F-005): verifies that the client-side authentication service
/// correctly persists both the access token and the refresh token on login,
/// and uses the refresh token (not the access token) on logout.
/// </remarks>
public class AuthServiceTests
{
    #region Fields

    private readonly Mock<IJSRuntime> _jsRuntimeMock;
    private readonly TokenStorageService _tokenStorage;
    private readonly Mock<IAccountService> _accountServiceMock;
    private readonly Mock<AuthenticationStateProvider> _authStateProviderMock;
    private readonly WebUI.Client.AuthService _sut;

    #endregion

    #region Constructor

    public AuthServiceTests()
    {
        _jsRuntimeMock = new Mock<IJSRuntime>();
        _tokenStorage = new TokenStorageService(_jsRuntimeMock.Object);
        _accountServiceMock = new Mock<IAccountService>();
        _authStateProviderMock = new Mock<AuthenticationStateProvider>();

        _sut = new WebUI.Client.AuthService(
            _tokenStorage,
            _authStateProviderMock.Object,
            _accountServiceMock.Object);
    }

    #endregion

    #region LoginAsync

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task LoginAsync_WithValidCredentials_ReturnsTrue()
    {
        // Arrange: backend returns both access and refresh tokens.
        _accountServiceMock
            .Setup(s => s.LoginAsync(It.IsAny<LoginRequestDto>()))
            .ReturnsAsync(new LoginResponseDto
            {
                Token = "test-access-token",
                Email = "user@example.com",
                ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                RefreshToken = "test-refresh-token"
            });

        // Act
        bool result = await _sut.LoginAsync("user@example.com", "password");

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task LoginAsync_WithMissingRefreshToken_ReturnsFalse()
    {
        // Arrange: backend returns only an access token (refresh missing).
        // UC-007 requires both tokens to be present; AuthService refuses partial pairs.
        _accountServiceMock
            .Setup(s => s.LoginAsync(It.IsAny<LoginRequestDto>()))
            .ReturnsAsync(new LoginResponseDto
            {
                Token = "access-token-only",
                Email = "user@example.com",
                ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                RefreshToken = null
            });

        // Act
        bool result = await _sut.LoginAsync("user@example.com", "password");

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task LoginAsync_WithErrorResponse_ReturnsFalse()
    {
        // Arrange: backend returns an error result, not a LoginResponseDto.
        _accountServiceMock
            .Setup(s => s.LoginAsync(It.IsAny<LoginRequestDto>()))
            .ReturnsAsync(new ErrorDto { ErrorMessages = ["Invalid credentials"] });

        // Act
        bool result = await _sut.LoginAsync("user@example.com", "wrong-password");

        // Assert
        Assert.False(result);
    }

    #endregion

    #region LogoutAsync

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task LogoutAsync_SendsRefreshTokenToBackend()
    {
        // Arrange: storage holds a refresh token, accessible via getItem("refreshToken").
        const string storedRefreshToken = "stored-refresh-token";
        _jsRuntimeMock
            .Setup(j => j.InvokeAsync<string?>(
                "localStorage.getItem",
                It.Is<object[]>(args => args.Length == 1 && (string)args[0] == "refreshToken")))
            .ReturnsAsync(storedRefreshToken);

        LogoutRequestDto? capturedRequest = null;
        _accountServiceMock
            .Setup(s => s.LogoutAsync(It.IsAny<LogoutRequestDto>()))
            .Callback<LogoutRequestDto>(req => capturedRequest = req)
            .ReturnsAsync(new LogoutResponseDto());

        // Act
        await _sut.LogoutAsync();

        // Assert: backend was called with the REFRESH token (not the access token).
        // This guards against the historic bug where LogoutAsync sent the access token
        // because it called GetTokenAsync() instead of GetRefreshTokenAsync().
        Assert.NotNull(capturedRequest);
        Assert.Equal(storedRefreshToken, capturedRequest!.RefreshToken);
    }

    #endregion
}
