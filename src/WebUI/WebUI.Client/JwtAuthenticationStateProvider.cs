// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Components.Authorization;

using WebUI.Client.Services;

namespace WebUI.Client;

/// <summary>
/// Provides authentication state based on a JWT stored in local storage.
/// </summary>
/// <remarks>
/// This provider reads a JWT from the <see cref="TokenStorageService"/> and parses claims to determine the current authentication state for Blazor WebAssembly applications.
/// </remarks>
public class JwtAuthenticationStateProvider(TokenStorageService tokenStorageService) : AuthenticationStateProvider
{
    /// <summary>
    /// Asynchronously gets the current authentication state based on the JWT in local storage.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the current <see cref="AuthenticationState"/>.</returns>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? token = await tokenStorageService.GetTokenAsync();
        ClaimsIdentity identity = string.IsNullOrWhiteSpace(token)
            ? new ClaimsIdentity()
            : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        ClaimsPrincipal user = new(identity);
        return new AuthenticationState(user);
    }

    /// <summary>
    /// Notifies subscribers that the authentication state has changed.
    /// </summary>
    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    /// <summary>
    /// Parses claims from a JWT.
    /// </summary>
    /// <param name="jwt">A JWT string to parse.</param>
    /// <returns>An enumerable collection of <see cref="Claim"/> parsed from the JWT.</returns>
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken token = handler.ReadJwtToken(jwt);
        return token.Claims;
    }
}
