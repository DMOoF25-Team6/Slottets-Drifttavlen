// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.


using System.Net;
using System.Net.Http.Json;

using Core.Providers;

using Infrastructure.Managers;

using Moq;
using Moq.Protected;


namespace Infrastructure.Tests.Managers;


/// <summary>
/// Unit tests for <see cref="DatabaseConnectionManager"/> using xUnit and Moq.
/// </summary>

public class DatabaseConnectionManagerTests
{
    #region Helpers
    /// <summary>
    /// Creates a mock <see cref="HttpClient"/> that returns the specified response.
    /// </summary>
    private static HttpClient CreateMockHttpClient(HttpResponseMessage response)
    {
        Mock<HttpMessageHandler> handlerMock = new(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response)
            .Verifiable();
        return new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://localhost")
        };
    }
    #endregion

    #region Functionality
    /// <summary>
    /// Should update state provider to true when API returns connected.
    /// </summary>
    [Fact]
    [Trait("Category", "Functionality")]
    public async Task CheckAndUpdateConnectionStateAsync_Connected_UpdatesStateProviderTrue()
    {
        // Arrange
        HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(true)
        };
        HttpClient client = CreateMockHttpClient(response);
        DatabaseConnectionStateProvider stateProvider = new();
        DatabaseConnectionManager manager = new(client, stateProvider);

        // Act
        await manager.CheckAndUpdateConnectionStateAsync();

        // Assert
        Assert.True(stateProvider.IsConnected);
    }
    #endregion

    #region EdgeCase
    /// <summary>
    /// Should update state provider to false when API returns not connected.
    /// </summary>
    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task CheckAndUpdateConnectionStateAsync_NotConnected_UpdatesStateProviderFalse()
    {
        // Arrange
        HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(false)
        };
        HttpClient client = CreateMockHttpClient(response);
        DatabaseConnectionStateProvider stateProvider = new();
        DatabaseConnectionManager manager = new(client, stateProvider);

        // Act
        await manager.CheckAndUpdateConnectionStateAsync();

        // Assert
        Assert.False(stateProvider.IsConnected);
    }

    /// <summary>
    /// Should set state provider to false if API throws an exception.
    /// </summary>
    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task CheckAndUpdateConnectionStateAsync_ApiThrows_SetsStateProviderFalse()
    {
        // Arrange
        Mock<HttpMessageHandler> handlerMock = new(MockBehavior.Strict);
        _ = handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("API error"));
        HttpClient client = new(handlerMock.Object)
        {
            BaseAddress = new Uri("http://localhost")
        };
        DatabaseConnectionStateProvider stateProvider = new();
        DatabaseConnectionManager manager = new(client, stateProvider);

        // Act
        await manager.CheckAndUpdateConnectionStateAsync();

        // Assert
        Assert.False(stateProvider.IsConnected);
    }
    #endregion
}
