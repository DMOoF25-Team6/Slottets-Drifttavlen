// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Components.Authorization;

using WebUI.Client.Services;

namespace WebUI.Client;

/// <summary>
/// Provides authentication state based on JWT stored in local storage.
/// </summary>
public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly TokenStorageService _tokenStorageService;

    public JwtAuthenticationStateProvider(TokenStorageService tokenStorageService)
    {
        _tokenStorageService = tokenStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string? token = await _tokenStorageService.GetTokenAsync();
        ClaimsIdentity identity = string.IsNullOrWhiteSpace(token)
            ? new ClaimsIdentity()
            : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        ClaimsPrincipal user = new(identity);
        return new AuthenticationState(user);
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken token = handler.ReadJwtToken(jwt);
        return token.Claims;
    }
}
