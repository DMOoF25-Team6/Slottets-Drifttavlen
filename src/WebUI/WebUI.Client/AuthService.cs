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
    /// Attempts to log in with the provided credentials. Stores JWT and updates authentication state on success.
    /// </summary>
    public async Task<bool> LoginAsync(string username, string password)
    {
        LoginRequestDto userLogin = new() { Email = username, Password = password };
        ILoginResult result = await accountService.LoginAsync(userLogin);
        if (result is LoginResponseDto loginResponse && loginResponse.Token is not null)
        {
            await tokenStorageService.SetTokenAsync(loginResponse.Token);
            (authenticationStateProvider as JwtAuthenticationStateProvider)?.NotifyAuthenticationStateChanged();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Logs out the current user by removing the JWT and updating authentication state.
    /// </summary>
    public async Task LogoutAsync()
    {
        LogoutRequestDto logoutRequest = new() { RefreshToken = await tokenStorageService.GetTokenAsync() ?? string.Empty };
        _ = await accountService.LogoutAsync(logoutRequest);
        await tokenStorageService.RemoveTokenAsync();
        (authenticationStateProvider as JwtAuthenticationStateProvider)?.NotifyAuthenticationStateChanged();
    }
}
