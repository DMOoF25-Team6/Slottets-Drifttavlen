// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Identity;
using Core.Interfaces.Dto.Identity;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;

namespace Core.Services;

/// <summary>
/// Provides Account-related operations such as registration, login, logout, and token refresh.
/// </summary>
/// <remarks>
/// This service delegates Account management operations to the <see cref="IAccountManager"/> implementation.
/// </remarks>
public class AccountService(IAccountManager AccountManager) : IAccountService
{
    private readonly IAccountManager _AccountManager = AccountManager;

    /// <summary>
    /// Creates a new user Account.
    /// </summary>
    /// <param name="registrationRequestDto">A registration request DTO containing user details.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="RegistrationResponseDto"/>.</returns>
    public async Task<RegistrationResponseDto> CreateAccountAsync(RegisterRequestDto registrationRequestDto)
    {
        return await _AccountManager.CreateAccountAsync(registrationRequestDto);
    }

    /// <summary>
    /// Refreshes the authentication token for a user.
    /// </summary>
    /// <param name="refreshTokenRequestDto">A refresh token request DTO containing the refresh token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="RefreshTokenResponseDto"/>.</returns>
    public async Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        return await _AccountManager.RefreshTokenAsync(refreshTokenRequestDto);
    }

    public async Task<ILoginResult> LoginAsync(LoginRequestDto loginRequestDto)
    {
        ILoginResult result = await _AccountManager.LoginAsync(loginRequestDto);
        if (result is LoginResponseDto loginResponse)
        {
            return loginResponse;
        }
        // If result is an error, map to LoginResponseDto with empty/null tokens and error info if needed
        return new ErrorDto
        {
            ErrorMessages = ["Login failed"]
            // You can include additional error details here if needed
        };
    }

    public async Task<ILogoutResult> LogoutAsync(LogoutRequestDto logoutRequestDto)
    {
        ILogoutResult result = await _AccountManager.LogoutAsync(logoutRequestDto);
        if (result is LogoutResponseDto logoutResponse)
        {
            return (ILogoutResult)logoutResponse;
        }
        // If result is an error, map to LogoutResponseDto with empty/null tokens and error info if needed
        return new ErrorDto
        {
            ErrorMessages = ["Logout failed"]
            // You can include additional error details here if needed
        };
    }
}
