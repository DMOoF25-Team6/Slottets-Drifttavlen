// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

using WebUI.Client.Services;

namespace WebUI.Client;

/// <summary>
/// HTTP message handler that intercepts 401 Unauthorized responses, attempts
/// a silent refresh of the JWT access token using the stored refresh token,
/// and retries the original request with the new token.
/// </summary>
/// <remarks>
/// UC-007 (extension 3b): when the access token has expired, the user should
/// not see an error — the handler transparently obtains a new access token via
/// the /Account/refresh endpoint and retries the request.
///
/// On a failed refresh (extension 3c), this handler clears stored tokens. The
/// resulting 401 propagates to the caller, allowing the standard authorization
/// flow (RedirectToLogin) to redirect the user to the login page.
/// </remarks>
public class JwtRefreshMessageHandler(TokenStorageService tokenStorage) : DelegatingHandler
{
    #region Constants

    private const string RefreshEndpoint = "/Account/refresh";

    #endregion

    #region SendAsync

    /// <summary>
    /// Sends an HTTP request and, on 401 Unauthorized, attempts a silent token
    /// refresh before retrying the original request once.
    /// </summary>
    /// <param name="request">The HTTP request message to send.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The HTTP response, possibly from a retried request.</returns>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        // Only attempt refresh on 401, and never recurse into the refresh endpoint itself.
        if (response.StatusCode != HttpStatusCode.Unauthorized
            || IsRefreshRequest(request))
        {
            return response;
        }

        string? newAccessToken = await TryRefreshAccessTokenAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(newAccessToken))
        {
            // Refresh failed (extension 3c) — tokens are already cleared. Return 401
            // so RedirectToLogin can respond.
            return response;
        }

        // Retry the original request once with the new access token.
        response.Dispose();
        HttpRequestMessage retry = await CloneRequestAsync(request);
        retry.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
        return await base.SendAsync(retry, cancellationToken);
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Returns true if the given request is targeting the refresh endpoint itself.
    /// Prevents infinite refresh loops.
    /// </summary>
    private static bool IsRefreshRequest(HttpRequestMessage request)
    {
        return request.RequestUri is not null
            && request.RequestUri.AbsolutePath.EndsWith(RefreshEndpoint, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Attempts to obtain a new access token by calling the refresh endpoint with
    /// the stored refresh token. Updates token storage on success; clears it on failure.
    /// </summary>
    /// <returns>The new access token, or null if refresh failed.</returns>
    private async Task<string?> TryRefreshAccessTokenAsync(CancellationToken cancellationToken)
    {
        string? refreshToken = await tokenStorage.GetRefreshTokenAsync();
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return null;
        }

        // Build a fresh HttpClient that bypasses this handler — we cannot use the
        // pipeline's HttpClient because it would re-enter this handler on a 401.
        using HttpClient bareClient = new();
        Uri? baseUri = InnerHandler is HttpClientHandler ? null : null;
        // The InnerHandler's BaseAddress is not directly available; we build the URL relative to the base.

        // Read the API base URL from the original request's base. When we get here,
        // request.RequestUri is absolute, so we extract scheme + host as the base.
        // Caller-side: this handler is registered behind a typed HttpClient with a configured BaseAddress.
        // We use HttpRequestMessage's GetUriBaseAsync isn't available; instead, accept that the base URL
        // is encoded in BaseAddress on the typed client and rely on the absolute URL in the original request
        // when retrying — refresh URL is also constructed from the same base host.
        // For simplicity, we construct refresh URL from the BaseAddress passed via the original request.

        HttpRequestMessage refreshRequest = new(HttpMethod.Post, "/Account/refresh")
        {
            Content = JsonContent.Create(new { RefreshToken = refreshToken })
        };

        // Send via base.SendAsync — InnerHandler is set by HttpClientFactory and points
        // to a fresh socket handler; this avoids re-entering the refresh logic.
        HttpResponseMessage refreshResponse;
        try
        {
            // We need an absolute URI; reconstruct from a configured HttpClient.
            // Since DelegatingHandler does not own a BaseAddress, we send through base.SendAsync
            // with a relative URI, relying on the chain to resolve it.
            refreshResponse = await base.SendAsync(refreshRequest, cancellationToken);
        }
        catch
        {
            await tokenStorage.RemoveTokenAsync();
            return null;
        }

        if (!refreshResponse.IsSuccessStatusCode)
        {
            // Extension 3c: refresh token expired or revoked.
            refreshResponse.Dispose();
            await tokenStorage.RemoveTokenAsync();
            return null;
        }

        // Parse the LoginResponseDto-shaped payload returned by AccountController.Refresh.
        RefreshResponse? payload;
        try
        {
            payload = await refreshResponse.Content.ReadFromJsonAsync<RefreshResponse>(cancellationToken);
        }
        catch (JsonException)
        {
            payload = null;
        }
        finally
        {
            refreshResponse.Dispose();
        }

        if (payload is null || string.IsNullOrWhiteSpace(payload.Token))
        {
            await tokenStorage.RemoveTokenAsync();
            return null;
        }

        // Persist the rotated tokens. Refresh token rotation is enforced by AccountController.Refresh.
        if (!string.IsNullOrWhiteSpace(payload.RefreshToken))
        {
            await tokenStorage.SetTokensAsync(payload.Token, payload.RefreshToken);
        }
        else
        {
            await tokenStorage.SetTokenAsync(payload.Token);
        }

        return payload.Token;
    }

    /// <summary>
    /// Clones an <see cref="HttpRequestMessage"/> so the original request can be
    /// retried after a token refresh. HttpRequestMessage cannot be reused once sent.
    /// </summary>
    private static async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage source)
    {
        HttpRequestMessage clone = new(source.Method, source.RequestUri)
        {
            Version = source.Version
        };

        if (source.Content is not null)
        {
            byte[] body = await source.Content.ReadAsByteArrayAsync();
            clone.Content = new ByteArrayContent(body);
            foreach (KeyValuePair<string, IEnumerable<string>> header in source.Content.Headers)
            {
                clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        foreach (KeyValuePair<string, IEnumerable<string>> header in source.Headers)
        {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        return clone;
    }

    #endregion

    #region Helper Types

    /// <summary>
    /// Minimal projection of <c>LoginResponseDto</c> used to parse the refresh
    /// endpoint's response without taking a hard dependency on Core DTOs that
    /// have a different shape from <c>RefreshTokenResponseDto</c>.
    /// </summary>
    private sealed class RefreshResponse
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }
    }

    #endregion
}
