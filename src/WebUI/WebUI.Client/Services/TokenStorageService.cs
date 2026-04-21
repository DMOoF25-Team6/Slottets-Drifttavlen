// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Microsoft.JSInterop;

namespace WebUI.Client.Services;

/// <summary>
/// Service for storing and retrieving JWT tokens in browser local storage.
/// </summary>
public class TokenStorageService(IJSRuntime jsRuntime)
{
    private const string TokenKey = "jwt_token";

    public ValueTask SetTokenAsync(string token)
        => jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);

    public ValueTask<string?> GetTokenAsync()
        => jsRuntime.InvokeAsync<string?>("localStorage.getItem", TokenKey);

    public ValueTask RemoveTokenAsync()
        => jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
}
