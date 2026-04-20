// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Account;

namespace Core.Interfaces.Services;

/// <summary>
/// Provides account-related operations such as registration, login, token refresh, and logout.
/// </summary>
/// <remarks>
/// This service encapsulates authentication and authorization logic for user accounts.
/// </remarks>
/// <seealso cref="RegistrationRequestDto"/>
/// <seealso cref="LoginRequestDto"/>
/// <seealso cref="RefreshTokenRequestDto"/>
/// <seealso cref="LogoutRequestDto"/>
public interface IAccountService
{
    /// <summary>
    /// Creates a new user account based on the provided registration information.
    /// </summary>
    /// <param name="registrationRequestDto">A registration request containing user details.</param>
    /// <returns>A response containing registration result details.</returns>
    /// <example>
    /// <code language="csharp">
    /// var request = new RegistrationRequestDto { Email = "user@example.com", Password = "P@ssw0rd!" };
    /// var response = await accountService.CreateAccountAsync(request);
    /// </code>
    /// </example>
    Task<RegistrationResponseDto> CreateAccountAsync(RegistrationRequestDto registrationRequestDto);

    /// <summary>
    /// Authenticates a user and returns a login response with tokens.
    /// </summary>
    /// <param name="loginRequestDto">A login request containing user credentials.</param>
    /// <returns>A response containing authentication tokens and user information.</returns>
    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);

    /// <summary>
    /// Refreshes authentication tokens using a valid refresh token.
    /// </summary>
    /// <param name="refreshTokenRequestDto">A refresh token request containing the current refresh token.</param>
    /// <returns>A response containing new authentication tokens.</returns>
    Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto);

    /// <summary>
    /// Logs out the user and invalidates the current session or refresh token.
    /// </summary>
    /// <param name="logoutRequestDto">A logout request containing session or token information.</param>
    /// <returns>A response indicating the result of the logout operation.</returns>
    Task<LogoutResponseDto> LogoutAsync(LogoutRequestDto logoutRequestDto);
}
