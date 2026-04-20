// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Headers;

using WebUI.Client.Services;

namespace WebUI.Client;

/// <summary>
/// HTTP message handler that attaches a JWT from local storage to outgoing HTTP requests.
/// </summary>
/// <remarks>
/// This handler retrieves a JWT from the <see cref="TokenStorageService"/> and adds it as a Bearer token to the <c>Authorization</c> header of outgoing requests.
/// </remarks>
public class JwtAuthorizationMessageHandler(TokenStorageService tokenStorageService) : DelegatingHandler
{
    /// <summary>
    /// Sends an HTTP request with a JWT Bearer token attached if available.
    /// </summary>
    /// <param name="request">An HTTP request message to send.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the HTTP response message.</returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string? token = await tokenStorageService.GetTokenAsync();
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}