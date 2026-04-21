// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Identity;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;

namespace Core.Services;

/// <summary>
/// Provides account-related operations such as registration, login, logout, and token refresh.
/// </summary>
/// <remarks>
/// This service delegates account management operations to the <see cref="IAccountManager"/> implementation.
/// </remarks>
public class AccountService(IAccountManager accountManager) : IAccountService
{
    private readonly IAccountManager _accountManager = accountManager;

    /// <summary>
    /// Creates a new user account.
    /// </summary>
    /// <param name="registrationRequestDto">A registration request DTO containing user details.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="RegistrationResponseDto"/>.</returns>
    public Task<RegistrationResponseDto> CreateAccountAsync(RegisterRequestDto registrationRequestDto)
    {
        return _accountManager.CreateAccountAsync(registrationRequestDto);
    }

    /// <summary>
    /// Authenticates a user and returns a login response.
    /// </summary>
    /// <param name="loginRequestDto">A login request DTO containing user credentials.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="LoginResponseDto"/>.</returns>
    public Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        return _accountManager.LoginAsync(loginRequestDto);
    }

    /// <summary>
    /// Logs out a user and invalidates their session.
    /// </summary>
    /// <param name="logoutRequestDto">A logout request DTO containing user information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="LogoutResponseDto"/>.</returns>
    public Task<LogoutResponseDto> LogoutAsync(LogoutRequestDto logoutRequestDto)
    {
        return _accountManager.LogoutAsync(logoutRequestDto);
    }

    /// <summary>
    /// Refreshes the authentication token for a user.
    /// </summary>
    /// <param name="refreshTokenRequestDto">A refresh token request DTO containing the refresh token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="RefreshTokenResponseDto"/>.</returns>
    public Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        return _accountManager.RefreshTokenAsync(refreshTokenRequestDto);
    }
}
