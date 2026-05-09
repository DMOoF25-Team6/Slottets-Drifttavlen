// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Api.Controllers;

using Core.DTOs.Audit;
using Core.Interfaces.Repositories;

using Domain.Entities;

using Microsoft.AspNetCore.Mvc;

using Moq;

namespace WebApi.Tests.Controllers.Audit;

/// <summary>
/// Unit tests for the <see cref="AuditController"/>.
/// </summary>
/// <remarks>
/// Verifies HTTP status codes, repository interactions, and DTO mapping for the
/// audit endpoints used by UC-009 (View History and Traceability).
/// Uses <see cref="Moq"/> to isolate the controller from the data layer.
/// </remarks>
public class AuditControllerTests
{
    #region Fields

    private readonly Mock<IAuditRepository> _mockRepo;
    private readonly AuditController _controller;

    #endregion

    #region Constructor

    public AuditControllerTests()
    {
        _mockRepo = new Mock<IAuditRepository>(MockBehavior.Strict);
        _controller = new AuditController(_mockRepo.Object);
    }

    #endregion

    #region Helpers

    private static AuditEntry CreateEntry(string metadata = "Resident - Added", Guid? id = null)
    {
        return new AuditEntry
        {
            Id = id ?? Guid.NewGuid(),
            Metadata = metadata,
            StartTimeUtc = DateTime.UtcNow,
            EndTimeUtc = DateTime.UtcNow.AddSeconds(1),
            Succeeded = true,
            UserId = Guid.NewGuid(),
            ChangeDetails = []
        };
    }

    #endregion

    #region GetRecent Tests

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetRecent_WhenEntriesExist_ReturnsOkWithMappedDtos()
    {
        // Arrange
        List<AuditEntry> entries =
        [
            CreateEntry("Resident - Added"),
            CreateEntry("ResidentNote - Modified")
        ];
        _ = _mockRepo
            .Setup(r => r.GetRecentAsync(50, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entries);

        // Act
        IActionResult result = await _controller.GetRecent(50, CancellationToken.None);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        IEnumerable<AuditEntryDto> dtos = Assert.IsAssignableFrom<IEnumerable<AuditEntryDto>>(okResult.Value);
        Assert.Equal(2, dtos.Count());
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetRecent_WithNullLimit_PassesNullToRepository()
    {
        // Arrange
        _ = _mockRepo
            .Setup(r => r.GetRecentAsync(null, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        IActionResult result = await _controller.GetRecent(null, CancellationToken.None);

        // Assert
        _ = Assert.IsType<OkObjectResult>(result);
        _mockRepo.Verify(r => r.GetRecentAsync(null, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GetRecent_WhenNoEntriesExist_ReturnsOkWithEmptyCollection()
    {
        // Arrange
        _ = _mockRepo
            .Setup(r => r.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        IActionResult result = await _controller.GetRecent(10, CancellationToken.None);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        IEnumerable<AuditEntryDto> dtos = Assert.IsAssignableFrom<IEnumerable<AuditEntryDto>>(okResult.Value);
        Assert.Empty(dtos);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetRecent_WithMetadata_SplitsIntoEntityAndChangeType()
    {
        // Arrange
        List<AuditEntry> entries = [CreateEntry("Resident - Added")];
        _ = _mockRepo
            .Setup(r => r.GetRecentAsync(It.IsAny<int?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entries);

        // Act
        IActionResult result = await _controller.GetRecent(10, CancellationToken.None);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        AuditEntryDto dto = Assert.Single(Assert.IsAssignableFrom<IEnumerable<AuditEntryDto>>(okResult.Value));
        Assert.Equal("Resident", dto.Entity);
        Assert.Equal("Added", dto.ChangeType);
    }

    #endregion

    #region GetByEntityName Tests

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetByEntityName_WhenEntriesExist_ReturnsOkWithMappedDtos()
    {
        // Arrange
        const string entityName = "Resident";
        List<AuditEntry> entries =
        [
            CreateEntry("Resident - Added"),
            CreateEntry("Resident - Modified")
        ];
        _ = _mockRepo
            .Setup(r => r.GetByEntityNameAsync(entityName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entries);

        // Act
        IActionResult result = await _controller.GetByEntityName(entityName, CancellationToken.None);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        IEnumerable<AuditEntryDto> dtos = Assert.IsAssignableFrom<IEnumerable<AuditEntryDto>>(okResult.Value);
        Assert.Equal(2, dtos.Count());
    }

    [Theory]
    [Trait("Category", "EdgeCase")]
    [InlineData("")]
    [InlineData("   ")]
    public async Task GetByEntityName_WhenEntityNameIsNullOrWhitespace_ReturnsBadRequest(string entityName)
    {
        // Act
        IActionResult result = await _controller.GetByEntityName(entityName, CancellationToken.None);

        // Assert
        _ = Assert.IsType<BadRequestObjectResult>(result);
        _mockRepo.Verify(
            r => r.GetByEntityNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GetByEntityName_WhenNoEntriesMatch_ReturnsOkWithEmptyCollection()
    {
        // Arrange
        const string entityName = "NonExistentEntity";
        _ = _mockRepo
            .Setup(r => r.GetByEntityNameAsync(entityName, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        IActionResult result = await _controller.GetByEntityName(entityName, CancellationToken.None);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        IEnumerable<AuditEntryDto> dtos = Assert.IsAssignableFrom<IEnumerable<AuditEntryDto>>(okResult.Value);
        Assert.Empty(dtos);
    }

    #endregion

    #region GetWithDetails Tests

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetWithDetails_WhenEntryExists_ReturnsOkWithDtoAndDetails()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        AuditEntry entry = CreateEntry("Resident - Modified", id);
        entry.ChangeDetails =
        [
            new ChangeDetail { Id = Guid.NewGuid(), Field = "Initials", OldValue = "AB", NewValue = "CD", AuditEntryId = id }
        ];
        _ = _mockRepo
            .Setup(r => r.GetWithDetailsAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entry);

        // Act
        IActionResult result = await _controller.GetWithDetails(id, CancellationToken.None);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result);
        AuditEntryDto dto = Assert.IsType<AuditEntryDto>(okResult.Value);
        Assert.Equal(id, dto.Id);
        Assert.Single(dto.ChangeDetails);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GetWithDetails_WhenEntryDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        _ = _mockRepo
            .Setup(r => r.GetWithDetailsAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((AuditEntry?)null);

        // Act
        IActionResult result = await _controller.GetWithDetails(id, CancellationToken.None);

        // Assert
        _ = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetWithDetails_AnyId_PassesIdToRepository()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        AuditEntry entry = CreateEntry("Resident - Added", id);
        _ = _mockRepo
            .Setup(r => r.GetWithDetailsAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(entry);

        // Act
        _ = await _controller.GetWithDetails(id, CancellationToken.None);

        // Assert
        _mockRepo.Verify(r => r.GetWithDetailsAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region Constructor Tests

    [Fact]
    [Trait("Category", "EdgeCase")]
    public void Constructor_WithNullRepository_ThrowsArgumentNullException()
    {
        // Act + Assert
        _ = Assert.Throws<ArgumentNullException>(() => new AuditController(null!));
    }

    #endregion
}
