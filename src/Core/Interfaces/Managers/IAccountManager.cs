// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Identity;
using Core.Interfaces.Dto.Identity;

namespace Core.Interfaces.Managers;

/// <summary>
/// Provides Account management operations such as registration, login, logout, and token refresh.
/// </summary>
public interface IAccountManager
{
    /// <summary>
    /// Creates a new user Account based on the provided registration information.
    /// </summary>
    /// <param name="registrationRequestDto">An object containing the registration details.</param>
    /// <returns>A response object containing the result of the registration operation.</returns>
    Task<RegistrationResponseDto> CreateAccountAsync(RegisterRequestDto registrationRequestDto);

    /// <summary>
    /// Authenticates a user and returns login information, including tokens if successful.
    /// </summary>
    /// <param name="loginRequestDto">An object containing the login credentials.</param>
    /// <returns>A response object containing the result of the login operation.</returns>
    Task<ILoginResult> LoginAsync(LoginRequestDto loginRequestDto);

    /// <summary>
    /// Logs out a user and invalidates the current session or token.
    /// </summary>
    /// <param name="logoutRequestDto">An object containing the logout request details.</param>
    /// <returns>A response object containing the result of the logout operation.</returns>
    Task<ILogoutResult> LogoutAsync(LogoutRequestDto logoutRequestDto);

    /// <summary>
    /// Refreshes the authentication token for a user session.
    /// </summary>
    /// <param name="refreshTokenRequestDto">An object containing the refresh token details.</param>
    /// <returns>A response object containing the result of the token refresh operation.</returns>
    Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto);
}
