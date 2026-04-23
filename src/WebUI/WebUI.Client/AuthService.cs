// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Identity;
using Core.Services;

using Microsoft.AspNetCore.Components.Authorization;

using WebUI.Client.Services;

namespace WebUI.Client;

/// <summary>
/// Service for handling authentication logic, including login and logout.
/// </summary>
public class AuthService(
    TokenStorageService tokenStorageService,
    AuthenticationStateProvider authenticationStateProvider,
    AccountService accountService
    )
{

    /// <summary>
    /// Attempts to log in with the provided credentials. Stores JWT and updates authentication state on success.
    /// </summary>
    public async Task<bool> LoginAsync(string username, string password)
    {
        LoginRequestDto userLogin = new() { Email = username, Password = password };
        LoginResponseDto result = await accountService.LoginAsync(userLogin);
        if (result.JwtToken is not null)
        {
            await tokenStorageService.SetTokenAsync(result.JwtToken);
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
        await tokenStorageService.RemoveTokenAsync();
        (authenticationStateProvider as JwtAuthenticationStateProvider)?.NotifyAuthenticationStateChanged();
    }
}
