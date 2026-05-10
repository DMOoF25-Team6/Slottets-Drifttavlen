// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Headers;

using Microsoft.JSInterop;

using Moq;

using WebUI.Client;
using WebUI.Client.Services;

namespace WebUI.Tests.Handlers;

/// <summary>
/// Unit tests for <see cref="JwtAuthorizationMessageHandler"/>.
/// </summary>
/// <remarks>
/// UC-007 (REQ-F-005): verifies that the handler attaches the stored JWT access
/// token as a Bearer token on every outgoing request, and that requests are
/// passed through unchanged when no token is stored.
/// </remarks>
public class JwtAuthorizationMessageHandlerTests
{
    #region Fields

    private readonly Mock<IJSRuntime> _jsRuntimeMock;
    private readonly TokenStorageService _tokenStorage;
    private readonly CapturingInnerHandler _innerHandler;
    private readonly JwtAuthorizationMessageHandler _sut;
    private readonly HttpMessageInvoker _invoker;

    #endregion

    #region Constructor

    public JwtAuthorizationMessageHandlerTests()
    {
        _jsRuntimeMock = new Mock<IJSRuntime>();
        _tokenStorage = new TokenStorageService(_jsRuntimeMock.Object);
        _innerHandler = new CapturingInnerHandler();
        _sut = new JwtAuthorizationMessageHandler(_tokenStorage)
        {
            InnerHandler = _innerHandler
        };
        _invoker = new HttpMessageInvoker(_sut);
    }

    #endregion

    #region SendAsync

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task SendAsync_WithStoredToken_AttachesBearerHeader()
    {
        // Arrange: storage has an access token.
        const string token = "test-jwt-access-token";
        _jsRuntimeMock
            .Setup(j => j.InvokeAsync<string?>(
                "localStorage.getItem",
                It.Is<object[]>(args => args.Length == 1 && (string)args[0] == "token")))
            .ReturnsAsync(token);

        HttpRequestMessage request = new(HttpMethod.Get, "https://localhost/Resource");

        // Act
        HttpResponseMessage response = await _invoker.SendAsync(request, CancellationToken.None);

        // Assert: the inner handler saw a request with "Authorization: Bearer {token}".
        Assert.NotNull(_innerHandler.LastRequest);
        AuthenticationHeaderValue? auth = _innerHandler.LastRequest!.Headers.Authorization;
        Assert.NotNull(auth);
        Assert.Equal("Bearer", auth!.Scheme);
        Assert.Equal(token, auth.Parameter);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task SendAsync_WithoutStoredToken_DoesNotAttachAuthorizationHeader()
    {
        // Arrange: storage returns null (no token).
        _jsRuntimeMock
            .Setup(j => j.InvokeAsync<string?>("localStorage.getItem", It.IsAny<object[]>()))
            .ReturnsAsync((string?)null);

        HttpRequestMessage request = new(HttpMethod.Get, "https://localhost/Resource");

        // Act
        HttpResponseMessage response = await _invoker.SendAsync(request, CancellationToken.None);

        // Assert: no Authorization header was added.
        Assert.NotNull(_innerHandler.LastRequest);
        Assert.Null(_innerHandler.LastRequest!.Headers.Authorization);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task SendAsync_WithEmptyStoredToken_DoesNotAttachAuthorizationHeader()
    {
        // Arrange: storage returns empty string (anomalous but possible).
        _jsRuntimeMock
            .Setup(j => j.InvokeAsync<string?>("localStorage.getItem", It.IsAny<object[]>()))
            .ReturnsAsync(string.Empty);

        HttpRequestMessage request = new(HttpMethod.Get, "https://localhost/Resource");

        // Act
        HttpResponseMessage response = await _invoker.SendAsync(request, CancellationToken.None);

        // Assert: empty token is treated as "no token".
        Assert.Null(_innerHandler.LastRequest!.Headers.Authorization);
    }

    #endregion

    #region Test Helper

    /// <summary>
    /// Inner handler that captures the request it receives and returns a stub 200 OK.
    /// Used to inspect what the handler-under-test actually sent downstream.
    /// </summary>
    private sealed class CapturingInnerHandler : DelegatingHandler
    {
        public HttpRequestMessage? LastRequest { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            LastRequest = request;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }

    #endregion
}
