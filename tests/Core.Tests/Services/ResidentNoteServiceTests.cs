// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Managers;
using Core.Services;

using Moq;

namespace Core.Tests.Services;

public class ResidentNoteServiceTests
{
    #region Fields

    private readonly Mock<IResidentNoteManager> _mockManager;
    private readonly ResidentNoteService _service;

    #endregion

    #region Constructors

    public ResidentNoteServiceTests()
    {
        _mockManager = new Mock<IResidentNoteManager>();
        _service = new ResidentNoteService(_mockManager.Object);
    }

    #endregion

    #region Methods

    [Fact]
    public async Task GetAllByResidentIdAsync_ReturnsNotesForResident()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        List<ResidentNoteDto> notes =
        [
            new ResidentNoteDto { Id = Guid.NewGuid(), Note = "Note 1" },
            new ResidentNoteDto { Id = Guid.NewGuid(), Note = "Note 2" }
        ];
        _ = _mockManager
            .Setup(m => m.GetAllByResidentIdAsync(residentId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(notes);

        // Act
        IEnumerable<ResidentNoteDto> result = await _service.GetAllByResidentIdAsync(residentId, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        _mockManager.Verify(m => m.GetAllByResidentIdAsync(residentId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AddAsync_ReturnsTrueWhenNoteAdded()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        _ = _mockManager
            .Setup(m => m.AddAsync(residentId, It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        bool result = await _service.AddAsync(residentId, "New Note", TestContext.Current.CancellationToken);

        // Assert
        Assert.True(result);
        _mockManager.Verify(m => m.AddAsync(residentId, "New Note", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsTrueWhenNoteUpdated()
    {
        // Arrange
        Guid noteId = Guid.NewGuid();
        _ = _mockManager
            .Setup(m => m.UpdateAsync(noteId, It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        bool result = await _service.UpdateAsync(noteId, "Updated text", TestContext.Current.CancellationToken);

        // Assert
        Assert.True(result);
        _mockManager.Verify(m => m.UpdateAsync(noteId, "Updated text", It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrueWhenNoteDeleted()
    {
        // Arrange
        Guid noteId = Guid.NewGuid();
        _ = _mockManager
            .Setup(m => m.DeleteAsync(noteId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        bool result = await _service.DeleteAsync(noteId, TestContext.Current.CancellationToken);

        // Assert
        Assert.True(result);
        _mockManager.Verify(m => m.DeleteAsync(noteId, It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion
}
