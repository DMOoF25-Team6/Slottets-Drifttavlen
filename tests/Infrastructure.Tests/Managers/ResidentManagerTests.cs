// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Json;

using Core.DTOs;

using Domain.Entities;

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
    public async Task CreateAsync_ValidResident_ReturnsResident()
    {
        // Arrange
        Resident resident = new()
        {
            Id = Guid.NewGuid(),
            Initials = "JD",
            FirstName = "John",
            LastName = "Doe",
            TrafficLightStatus = null
        };
        ResidentResponseDto dto = new()
        {
            Id = resident.Id,
            Initials = resident.Initials,
            FirstName = resident.FirstName,
            LastName = resident.LastName,
            TrafficLightStatus = null,
            Notes = []
        };
        HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(dto)
        };
        _ = _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act
        Resident result = await _residentManager.CreateAsync(resident, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(resident.Id, result.Id);
        Assert.Equal(resident.Initials, result.Initials);
        Assert.Equal(resident.FirstName, result.FirstName);
        Assert.Equal(resident.LastName, result.LastName);
    }
    #endregion

    #region EdgeCase
    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task CreateAsync_ApiReturnsNull_ThrowsInvalidOperationException()
    {
        // Arrange
        Resident resident = new()
        {
            Id = Guid.NewGuid(),
            Initials = "JD",
            FirstName = "John",
            LastName = "Doe",
            TrafficLightStatus = null
        };
        HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = JsonContent.Create<ResidentResponseDto?>(null)
        };
        _ = _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        // Act & Assert
        _ = await Assert.ThrowsAsync<InvalidOperationException>(() => _residentManager.CreateAsync(resident, TestContext.Current.CancellationToken));
    }
    #endregion
}
