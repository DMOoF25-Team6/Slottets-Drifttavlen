// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.Net;

using Core.DTOs;

using Infrastructure.Managers;

using Moq;
using Moq.Protected;

namespace Infrastructure.Tests.Managers;

public class ResidentManagerTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly ResidentManager _residentManager;

    public ResidentManagerTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("http://localhost/")
        };
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _ = _httpClientFactoryMock.Setup(f => f.CreateClient("SlottetApi")).Returns(_httpClient);
        _residentManager = new ResidentManager(_httpClientFactoryMock.Object);
    }

    #region Functionality

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task CreateAsync_ValidResidentRequest_Success()
    {
        // Arrange
        ResidentCreateRequestDto dto = new()
        {
            Initials = "JD",
            FirstName = "John",
            LastName = "Doe",
            TrafficLightStatus = null
        };
        using HttpResponseMessage response = new(HttpStatusCode.OK);
        _ = _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains("residents/Create")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act
        Exception? exception = await Record.ExceptionAsync(() => _residentManager.CreateAsync(dto, CancellationToken.None));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task CreateRangeAsync_ValidResidents_Success()
    {
        // Arrange
        ResidentCreateRequestDto[] dtos = new[]
        {
            new ResidentCreateRequestDto { Initials = "JD", FirstName = "John", LastName = "Doe" },
            new ResidentCreateRequestDto { Initials = "AS", FirstName = "Alice", LastName = "Smith" }
        };
        using HttpResponseMessage response = new(HttpStatusCode.OK);
        _ = _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains("residents/CreateRange")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act
        Exception? exception = await Record.ExceptionAsync(() => _residentManager.CreateRangeAsync(dtos, CancellationToken.None));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetByIdAsync_ResidentExists_ReturnsResident()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        ResidentResponseDto expected = new() { Id = id, Initials = "JD", FirstName = "John", LastName = "Doe" };
        string json = System.Text.Json.JsonSerializer.Serialize(expected);
        using HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };
        _ = _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains($"residents/{id}")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act
        ResidentResponseDto? result = await _residentManager.GetByIdAsync(id, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected.Id, result!.Id);
        Assert.Equal(expected.Initials, result.Initials);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetAllAsync_ResidentsExist_ReturnsAllResidents()
    {
        // Arrange
        ResidentResponseDto[] expected = new[]
        {
            new ResidentResponseDto { Id = Guid.NewGuid(), Initials = "JD", FirstName = "John", LastName = "Doe" },
            new ResidentResponseDto { Id = Guid.NewGuid(), Initials = "AS", FirstName = "Alice", LastName = "Smith" }
        };
        string json = System.Text.Json.JsonSerializer.Serialize(expected);
        using HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };
        _ = _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains("residents")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act
        IEnumerable<ResidentResponseDto> result = await _residentManager.GetAllAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, System.Linq.Enumerable.Count(result));
    }

    #endregion

    #region EdgeCase

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task CreateAsync_ApiReturnsError_ThrowsException()
    {
        // Arrange
        ResidentCreateRequestDto dto = new() { Initials = "JD", FirstName = "John", LastName = "Doe" };
        using HttpResponseMessage response = new(HttpStatusCode.BadRequest);
        _ = _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act & Assert
        Exception ex = await Assert.ThrowsAsync<Exception>(() => _residentManager.CreateAsync(dto, CancellationToken.None));
        Assert.Contains("Failed to create resident", ex.Message);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task CreateRangeAsync_ApiReturnsError_ThrowsException()
    {
        // Arrange
        ResidentCreateRequestDto[] dtos = new[] { new ResidentCreateRequestDto { Initials = "JD", FirstName = "John", LastName = "Doe" } };
        using HttpResponseMessage response = new(HttpStatusCode.InternalServerError);
        _ = _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act & Assert
        Exception ex = await Assert.ThrowsAsync<Exception>(() => _residentManager.CreateRangeAsync(dtos, CancellationToken.None));
        Assert.Contains("Failed to create residents", ex.Message);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GetByIdAsync_ResidentNotFound_ReturnsNull()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        using HttpResponseMessage response = new(HttpStatusCode.NotFound)
        {
            Content = new StringContent("null", System.Text.Encoding.UTF8, "application/json")
        };
        _ = _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act
        ResidentResponseDto? result = await _residentManager.GetByIdAsync(id, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GetAllAsync_NoResidents_ReturnsEmpty()
    {
        // Arrange
        string json = "[]";
        using HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };
        _ = _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act
        IEnumerable<ResidentResponseDto> result = await _residentManager.GetAllAsync(CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task CreateAsync_CancellationRequested_ThrowsTaskCanceledException()
    {
        // Arrange
        ResidentCreateRequestDto dto = new() { Initials = "JD", FirstName = "John", LastName = "Doe" };
        using HttpResponseMessage response = new(HttpStatusCode.OK);
        CancellationTokenSource cts = new();
        cts.Cancel();
        _ = _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains("residents/Create")),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new TaskCanceledException());
        // Act & Assert
        _ = await Assert.ThrowsAsync<TaskCanceledException>(() => _residentManager.CreateAsync(dto, cts.Token));
    }

    #endregion

    #region Concurrency

    [Fact]
    [Trait("Category", "Concurrency")]
    public async Task CreateAsync_MultipleConcurrentCalls_AllSucceed()
    {
        // Arrange
        ResidentCreateRequestDto[] dtos = Enumerable.Range(0, 10).Select(i => new ResidentCreateRequestDto
        {
            Initials = $"U{i}",
            FirstName = $"User{i}",
            LastName = $"Test{i}",
            TrafficLightStatus = null
        }).ToArray();
        using HttpResponseMessage response = new(HttpStatusCode.OK);
        _ = _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act
        IEnumerable<Task> tasks = dtos.Select(dto => _residentManager.CreateAsync(dto, CancellationToken.None));
        Exception?[] results = await Task.WhenAll(tasks.Select(task => Record.ExceptionAsync(() => task).AsTask()));

        // Assert
        Assert.All(results, Assert.Null);
    }

    #endregion
}
