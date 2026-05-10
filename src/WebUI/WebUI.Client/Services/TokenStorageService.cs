// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Microsoft.JSInterop;

namespace WebUI.Client.Services;

/// <summary>
/// Service for storing and retrieving JWT access tokens and refresh tokens
/// in browser local storage.
/// </summary>
/// <remarks>
/// UC-007: persists both the short-lived access token (JWT) and the long-lived
/// refresh token. The access token is attached to outgoing requests by
/// <see cref="JwtAuthorizationMessageHandler"/>; the refresh token is consumed
/// by JwtRefreshMessageHandler when the access token has expired.
/// </remarks>
public class TokenStorageService(IJSRuntime jsRuntime)
{
    #region Constants

    private const string AccessTokenKey = "token";
    private const string RefreshTokenKey = "refreshToken";

    #endregion

    #region Access Token

    /// <summary>
    /// Stores the JWT access token in browser local storage.
    /// </summary>
    /// <param name="token">The JWT access token to store.</param>
    public ValueTask SetTokenAsync(string token)
        => jsRuntime.InvokeVoidAsync("localStorage.setItem", AccessTokenKey, token);

    /// <summary>
    /// Retrieves the JWT access token from browser local storage.
    /// </summary>
    /// <returns>The stored access token, or null if no token is present.</returns>
    public ValueTask<string?> GetTokenAsync()
        => jsRuntime.InvokeAsync<string?>("localStorage.getItem", AccessTokenKey);

    #endregion

    #region Refresh Token

    /// <summary>
    /// Stores the refresh token in browser local storage.
    /// </summary>
    /// <param name="refreshToken">The refresh token to store.</param>
    public ValueTask SetRefreshTokenAsync(string refreshToken)
        => jsRuntime.InvokeVoidAsync("localStorage.setItem", RefreshTokenKey, refreshToken);

    /// <summary>
    /// Retrieves the refresh token from browser local storage.
    /// </summary>
    /// <returns>The stored refresh token, or null if no token is present.</returns>
    public ValueTask<string?> GetRefreshTokenAsync()
        => jsRuntime.InvokeAsync<string?>("localStorage.getItem", RefreshTokenKey);

    #endregion

    #region Combined

    /// <summary>
    /// Stores both the access token and the refresh token in a single call.
    /// </summary>
    /// <param name="accessToken">The JWT access token to store.</param>
    /// <param name="refreshToken">The refresh token to store.</param>
    public async ValueTask SetTokensAsync(string accessToken, string refreshToken)
    {
        await SetTokenAsync(accessToken);
        await SetRefreshTokenAsync(refreshToken);
    }

    /// <summary>
    /// Removes both the access token and the refresh token from browser local storage.
    /// </summary>
    public async ValueTask RemoveTokenAsync()
    {
        await jsRuntime.InvokeVoidAsync("localStorage.removeItem", AccessTokenKey);
        await jsRuntime.InvokeVoidAsync("localStorage.removeItem", RefreshTokenKey);
    }

    #endregion
}
