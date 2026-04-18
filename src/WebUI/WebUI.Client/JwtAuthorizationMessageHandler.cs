// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Headers;

using WebUI.Client.Services;

namespace WebUI.Client;

/// <summary>
/// HTTP message handler that attaches JWT from local storage to outgoing requests.
/// </summary>
public class JwtAuthorizationMessageHandler : DelegatingHandler
{
    private readonly TokenStorageService _tokenStorageService;

    public JwtAuthorizationMessageHandler(TokenStorageService tokenStorageService)
    {
        _tokenStorageService = tokenStorageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string? token = await _tokenStorageService.GetTokenAsync();
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}