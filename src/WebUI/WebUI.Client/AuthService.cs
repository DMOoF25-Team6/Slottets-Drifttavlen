// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Identity;
using Core.Interfaces.Dto.Identity;
using Core.Interfaces.Services;

using Microsoft.AspNetCore.Components.Authorization;

using WebUI.Client.Services;

namespace WebUI.Client;

/// <summary>
/// Service for handling authentication logic, including login and logout.
/// </summary>
public class AuthService(
    TokenStorageService tokenStorageService,
    AuthenticationStateProvider authenticationStateProvider,
    IAccountService accountService
    )
{
    /// <summary>
    /// Attempts to log in with the provided credentials. On success, stores both
    /// the JWT access token and the refresh token, and updates authentication state.
    /// </summary>
    /// <param name="username">The user's email.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>True if login succeeded; otherwise false.</returns>
    public async Task<bool> LoginAsync(string username, string password)
    {
        LoginRequestDto userLogin = new() { Email = username, Password = password };
        ILoginResult result = await accountService.LoginAsync(userLogin);
        if (result is LoginResponseDto loginResponse
            && !string.IsNullOrWhiteSpace(loginResponse.Token)
            && !string.IsNullOrWhiteSpace(loginResponse.RefreshToken))
        {
            // UC-007: persist both access token and refresh token; the refresh token
            // is required by JwtRefreshMessageHandler for silent token renewal (3b).
            await tokenStorageService.SetTokensAsync(loginResponse.Token, loginResponse.RefreshToken);
            (authenticationStateProvider as JwtAuthenticationStateProvider)?.NotifyAuthenticationStateChanged();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Logs out the current user by sending the refresh token to the backend for
    /// revocation, then clears local token storage and updates authentication state.
    /// </summary>
    public async Task LogoutAsync()
    {
        // UC-007: logout must send the REFRESH token (so the backend can revoke it),
        // not the access token.
        string refreshToken = await tokenStorageService.GetRefreshTokenAsync() ?? string.Empty;
        LogoutRequestDto logoutRequest = new() { RefreshToken = refreshToken };
        _ = await accountService.LogoutAsync(logoutRequest);

        // Clear both access and refresh tokens from local storage.
        await tokenStorageService.RemoveTokenAsync();
        (authenticationStateProvider as JwtAuthenticationStateProvider)?.NotifyAuthenticationStateChanged();
    }
}
