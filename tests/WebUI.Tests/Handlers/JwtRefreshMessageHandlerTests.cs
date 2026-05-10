// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.JSInterop;

using Moq;

using WebUI.Client;
using WebUI.Client.Services;

namespace WebUI.Tests.Handlers;

/// <summary>
/// Unit tests for <see cref="JwtRefreshMessageHandler"/>.
/// </summary>
/// <remarks>
/// UC-007 (extensions 3a + 3c): verifies that the handler intercepts 401 responses,
/// silently refreshes the access token via /Account/refresh, and retries the original
/// request once with the new token; on a failed refresh, it clears stored tokens and
/// returns the 401 so RedirectToLogin can react.
/// </remarks>
public class JwtRefreshMessageHandlerTests
{
    #region Fields

    private readonly Mock<IJSRuntime> _jsRuntimeMock;
    private readonly TokenStorageService _tokenStorage;
    private readonly ScriptedInnerHandler _innerHandler;
    private readonly JwtRefreshMessageHandler _sut;
    private readonly HttpMessageInvoker _invoker;

    #endregion

    #region Constructor

    public JwtRefreshMessageHandlerTests()
    {
        _jsRuntimeMock = new Mock<IJSRuntime>();
        _tokenStorage = new TokenStorageService(_jsRuntimeMock.Object);
        _innerHandler = new ScriptedInnerHandler();
        _sut = new JwtRefreshMessageHandler(_tokenStorage)
        {
            InnerHandler = _innerHandler
        };
        _invoker = new HttpMessageInvoker(_sut);
    }

    #endregion

    #region SendAsync — pass-through

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task SendAsync_When200_PassesThroughWithoutRefresh()
    {
        // Arrange: inner returns 200 OK on the first call.
        _innerHandler.Enqueue(new HttpResponseMessage(HttpStatusCode.OK));

        HttpRequestMessage request = new(HttpMethod.Get, "https://localhost/Resource");

        // Act
        HttpResponseMessage response = await _invoker.SendAsync(request, CancellationToken.None);

        // Assert: only one inner call, no refresh attempt.
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(1, _innerHandler.CallCount);
    }

    #endregion

    #region SendAsync — successful refresh

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task SendAsync_When401AndRefreshSucceeds_RetriesOriginalRequest()
    {
        // Arrange: storage holds a refresh token.
        const string refreshToken = "stored-refresh-token";
        _jsRuntimeMock
            .Setup(j => j.InvokeAsync<string?>(
                "localStorage.getItem",
                It.Is<object[]>(args => (string)args[0] == "refreshToken")))
            .ReturnsAsync(refreshToken);

        // Inner handler scripted: 401 -> 200 (refresh) -> 200 (retry).
        _innerHandler.Enqueue(new HttpResponseMessage(HttpStatusCode.Unauthorized));
        _innerHandler.Enqueue(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(new
            {
                token = "new-access-token",
                refreshToken = "new-refresh-token"
            })
        });
        _innerHandler.Enqueue(new HttpResponseMessage(HttpStatusCode.OK));

        HttpRequestMessage request = new(HttpMethod.Get, "https://localhost/Resource");

        // Act
        HttpResponseMessage response = await _invoker.SendAsync(request, CancellationToken.None);

        // Assert: final response is 200 OK after retry; 3 inner calls (original 401, refresh, retry).
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(3, _innerHandler.CallCount);
    }

    #endregion

    #region SendAsync — failed refresh paths

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task SendAsync_When401AndNoRefreshTokenStored_Returns401WithoutRefreshAttempt()
    {
        // Arrange: storage has no refresh token.
        _jsRuntimeMock
            .Setup(j => j.InvokeAsync<string?>("localStorage.getItem", It.IsAny<object[]>()))
            .ReturnsAsync((string?)null);

        _innerHandler.Enqueue(new HttpResponseMessage(HttpStatusCode.Unauthorized));

        HttpRequestMessage request = new(HttpMethod.Get, "https://localhost/Resource");

        // Act
        HttpResponseMessage response = await _invoker.SendAsync(request, CancellationToken.None);

        // Assert: original 401 is returned; no refresh attempt was made.
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.Equal(1, _innerHandler.CallCount);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task SendAsync_When401AndRefreshFails_Returns401()
    {
        // Arrange: storage has a refresh token, but backend rejects the refresh.
        _jsRuntimeMock
            .Setup(j => j.InvokeAsync<string?>(
                "localStorage.getItem",
                It.Is<object[]>(args => (string)args[0] == "refreshToken")))
            .ReturnsAsync("expired-refresh-token");

        _innerHandler.Enqueue(new HttpResponseMessage(HttpStatusCode.Unauthorized));   // original
        _innerHandler.Enqueue(new HttpResponseMessage(HttpStatusCode.Unauthorized));   // refresh fails

        HttpRequestMessage request = new(HttpMethod.Get, "https://localhost/Resource");

        // Act
        HttpResponseMessage response = await _invoker.SendAsync(request, CancellationToken.None);

        // Assert: original 401 is propagated; no retry of the original request.
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.Equal(2, _innerHandler.CallCount);
    }

    #endregion

    #region SendAsync — recursion guard

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task SendAsync_WhenRefreshEndpointReturns401_DoesNotRecurse()
    {
        // Arrange: simulate a request that is itself targeting the refresh endpoint
        // and gets a 401. The handler must NOT try to refresh recursively.
        _jsRuntimeMock
            .Setup(j => j.InvokeAsync<string?>("localStorage.getItem", It.IsAny<object[]>()))
            .ReturnsAsync("any-token");

        _innerHandler.Enqueue(new HttpResponseMessage(HttpStatusCode.Unauthorized));

        HttpRequestMessage request = new(HttpMethod.Post, "https://localhost/Account/refresh");

        // Act
        HttpResponseMessage response = await _invoker.SendAsync(request, CancellationToken.None);

        // Assert: only 1 inner call — no recursion into another refresh.
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.Equal(1, _innerHandler.CallCount);
    }

    #endregion

    #region Test Helper

    /// <summary>
    /// Inner handler that returns scripted responses in FIFO order. Used to simulate
    /// downstream behavior (e.g., 401 then 200 with refresh payload then 200 retry).
    /// </summary>
    private sealed class ScriptedInnerHandler : DelegatingHandler
    {
        private readonly Queue<HttpResponseMessage> _responses = new();

        public int CallCount { get; private set; }

        public void Enqueue(HttpResponseMessage response) => _responses.Enqueue(response);

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            CallCount++;
            if (_responses.Count == 0)
            {
                throw new InvalidOperationException(
                    $"ScriptedInnerHandler ran out of responses on call {CallCount}.");
            }
            return Task.FromResult(_responses.Dequeue());
        }
    }

    #endregion
}
