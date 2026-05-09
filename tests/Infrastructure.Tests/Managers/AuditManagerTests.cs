// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;
using System.Text;
using System.Text.Json;

using Core.DTOs.Audit;

using Infrastructure.Managers;

using Moq;
using Moq.Protected;

namespace Infrastructure.Tests.Managers;

/// <summary>
/// Unit tests for the <see cref="AuditManager"/> infrastructure class.
/// </summary>
/// <remarks>
/// Verifies that <see cref="AuditManager"/> correctly issues HTTP requests against the
/// audit endpoints used by UC-009 (View History and Traceability) and deserialises the
/// responses into <see cref="AuditEntryDto"/> objects.
/// </remarks>
public class AuditManagerTests
{
    #region Fields

    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly AuditManager _auditManager;

    #endregion

    #region Constructor

    public AuditManagerTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("http://localhost/")
        };
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();
        _ = _httpClientFactoryMock.Setup(f => f.CreateClient("SlottetApi")).Returns(_httpClient);
        _auditManager = new AuditManager(_httpClientFactoryMock.Object);
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Creates an <see cref="HttpResponseMessage"/> with the given status code and JSON-serialised body.
    /// </summary>
    private static HttpResponseMessage JsonResponse<T>(HttpStatusCode statusCode, T payload)
    {
        string json = JsonSerializer.Serialize(payload);
        return new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
    }

    /// <summary>
    /// Sets up the mocked <see cref="HttpMessageHandler"/> to return <paramref name="response"/> for
    /// any request whose URL contains <paramref name="urlContains"/>.
    /// </summary>
    private void SetupResponse(string urlContains, HttpResponseMessage response)
    {
        _ = _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains(urlContains)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);
    }

    #endregion

    #region GetRecentAsync Tests

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetRecentAsync_WithLimit_BuildsUrlWithLimitQueryString()
    {
        // Arrange
        IEnumerable<AuditEntryDto> payload = [];
        using HttpResponseMessage response = JsonResponse(HttpStatusCode.OK, payload);
        SetupResponse("audit/recent?limit=25", response);

        // Act
        Exception? exception = await Record.ExceptionAsync(() => _auditManager.GetRecentAsync(25, CancellationToken.None));

        // Assert
        Assert.Null(exception);
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains("audit/recent?limit=25")),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetRecentAsync_WithoutLimit_BuildsUrlWithoutQueryString()
    {
        // Arrange
        IEnumerable<AuditEntryDto> payload = [];
        using HttpResponseMessage response = JsonResponse(HttpStatusCode.OK, payload);
        SetupResponse("audit/recent", response);

        // Act
        Exception? exception = await Record.ExceptionAsync(() => _auditManager.GetRecentAsync(null, CancellationToken.None));

        // Assert
        Assert.Null(exception);
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.RequestUri!.ToString().EndsWith("audit/recent")),
            ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetRecentAsync_WithEntries_DeserialisesPayload()
    {
        // Arrange
        Guid expectedId = Guid.NewGuid();
        AuditEntryDto[] payload =
        [
            new AuditEntryDto
            {
                Id = expectedId,
                Entity = "Resident",
                ChangeType = "Added",
                Description = "Resident - Added"
            }
        ];
        using HttpResponseMessage response = JsonResponse(HttpStatusCode.OK, payload);
        SetupResponse("audit/recent", response);

        // Act
        IEnumerable<AuditEntryDto> result = await _auditManager.GetRecentAsync(50, CancellationToken.None);

        // Assert
        AuditEntryDto first = Assert.Single(result);
        Assert.Equal(expectedId, first.Id);
        Assert.Equal("Resident", first.Entity);
        Assert.Equal("Added", first.ChangeType);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GetRecentAsync_WhenApiReturnsNullJson_ReturnsEmptyCollection()
    {
        // Arrange
        using HttpResponseMessage response = new(HttpStatusCode.OK)
        {
            Content = new StringContent("null", Encoding.UTF8, "application/json")
        };
        SetupResponse("audit/recent", response);

        // Act
        IEnumerable<AuditEntryDto> result = await _auditManager.GetRecentAsync(10, CancellationToken.None);

        // Assert
        Assert.Empty(result);
    }

    #endregion

    #region GetByEntityNameAsync Tests

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetByEntityNameAsync_AnyEntityName_BuildsCorrectUrl()
    {
        // Arrange
        IEnumerable<AuditEntryDto> payload = [];
        using HttpResponseMessage response = JsonResponse(HttpStatusCode.OK, payload);
        SetupResponse("audit/entity/Resident", response);

        // Act
        Exception? exception = await Record.ExceptionAsync(() => _auditManager.GetByEntityNameAsync("Resident", CancellationToken.None));

        // Assert
        Assert.Null(exception);
        _httpMessageHandlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains("audit/entity/Resident")),
            ItExpr.IsAny<CancellationToken>());
    }


    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetByEntityNameAsync_WithEntries_DeserialisesPayload()
    {
        // Arrange
        AuditEntryDto[] payload =
        [
            new AuditEntryDto { Entity = "Resident", ChangeType = "Modified" },
            new AuditEntryDto { Entity = "Resident", ChangeType = "Deleted" }
        ];
        using HttpResponseMessage response = JsonResponse(HttpStatusCode.OK, payload);
        SetupResponse("audit/entity/Resident", response);

        // Act
        IEnumerable<AuditEntryDto> result = await _auditManager.GetByEntityNameAsync("Resident", CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Count());
    }

    #endregion

    #region GetWithDetailsAsync Tests

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetWithDetailsAsync_WhenApiReturnsOk_ReturnsDeserialisedDto()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        AuditEntryDto payload = new()
        {
            Id = id,
            Entity = "Resident",
            ChangeType = "Modified",
            ChangeDetails =
            [
                new ChangeDetailDto { Field = "Initials", OldValue = "AB", NewValue = "CD" }
            ]
        };
        using HttpResponseMessage response = JsonResponse(HttpStatusCode.OK, payload);
        SetupResponse($"audit/{id}", response);

        // Act
        AuditEntryDto? result = await _auditManager.GetWithDetailsAsync(id, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result!.Id);
        Assert.Single(result.ChangeDetails);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GetWithDetailsAsync_WhenApiReturns404_ReturnsNull()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        using HttpResponseMessage response = new(HttpStatusCode.NotFound);
        SetupResponse($"audit/{id}", response);

        // Act
        AuditEntryDto? result = await _auditManager.GetWithDetailsAsync(id, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GetWithDetailsAsync_WhenApiReturns500_ThrowsHttpRequestException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        using HttpResponseMessage response = new(HttpStatusCode.InternalServerError);
        SetupResponse($"audit/{id}", response);

        // Act + Assert
        _ = await Assert.ThrowsAsync<HttpRequestException>(() => _auditManager.GetWithDetailsAsync(id, CancellationToken.None));
    }

    #endregion

    #region Constructor Tests

    [Fact]
    [Trait("Category", "EdgeCase")]
    public void Constructor_WithNullFactory_ThrowsArgumentNullException()
    {
        // Arrange + Act + Assert
        _ = Assert.Throws<NullReferenceException>(() => new AuditManager(null!));
    }

    #endregion
}
