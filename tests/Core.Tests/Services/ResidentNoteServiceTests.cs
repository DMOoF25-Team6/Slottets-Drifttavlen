// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;
using Core.Services;
using Core.DTOs;

using Domain.Entities;

using Moq;

namespace Core.Tests.Services;

public class ResidentNoteServiceTests
{

    private readonly Mock<IResidentNoteRepository> _mockRepo;
    private readonly ResidentNoteService _service;

    public ResidentNoteServiceTests()
    {
        _mockRepo = new Mock<IResidentNoteRepository>();
        _service = new ResidentNoteService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetAllByResidentIdAsync_ReturnsNotesForResident()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        List<ResidentNote> notes =
        [
            new ResidentNote { Id = Guid.NewGuid(), ResidentId = residentId, Note = "Note 1" },
            new ResidentNote { Id = Guid.NewGuid(), ResidentId = residentId, Note = "Note 2" }
        ];
        _ = _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
          .ReturnsAsync(notes);
        // Act
        IEnumerable<ResidentNoteDto> result = await _service.GetAllByResidentIdAsync(residentId, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task AddAsync_ReturnsTrueWhenNoteAdded()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        _ = _mockRepo.Setup(r => r.AddAsync(It.IsAny<ResidentNote>(), It.IsAny<CancellationToken>()))
         .ReturnsAsync(new ResidentNote());

        // Act
        bool result = await _service.AddAsync(residentId, "New Note", TestContext.Current.CancellationToken);

        // Assert
        Assert.True(result);
    }
    [Fact]
    public async Task UpdateAsync_ReturnsTrueWhenNoteUpdated()
    {
        //ARRANGE:
        // 1. Create residentId and noteId
        Guid residentId = Guid.NewGuid();
        Guid noteId = Guid.NewGuid();

        // 2. Create an existing note
        ResidentNote existingNote = new() { Id = noteId, ResidentId = residentId, Note = "Old text" };

        // 3. Mock GetByIdAsync - repository finds the note
        _ = _mockRepo.Setup(r => r.GetByIdAsync(noteId, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(existingNote);

        // 4. Mock UpdateAsync - repository updates the note
        _ = _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<ResidentNote>(), It.IsAny<CancellationToken>()))
                 .Returns(Task.CompletedTask);

        // ACT:
        bool result = await _service.UpdateAsync(residentId, noteId, "New text", TestContext.Current.CancellationToken);

        //ASSERT:
        Assert.True(result);
        // Verify that UpdateAsync was called exactly once
        _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<ResidentNote>(), It.IsAny<CancellationToken>()), Times.Once);

    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrueWhenNoteDeleted()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        Guid noteId = Guid.NewGuid();
        ResidentNote existingNote = new() { Id = noteId, ResidentId = residentId, Note = "Note to delete" };

        // Mock GetByIdAsync - repository finds the note
        _ = _mockRepo.Setup(r => r.GetByIdAsync(noteId, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(existingNote);

        // Mock DeleteAsync - repository deletes the note
        _ = _mockRepo.Setup(r => r.DeleteAsync(It.IsAny<ResidentNote>(), It.IsAny<CancellationToken>()))
                 .Returns(Task.CompletedTask);

        // Act
        bool result = await _service.DeleteAsync(noteId, TestContext.Current.CancellationToken);

        // Assert
        Assert.True(result);
        // Verify that DeleteAsync was called exactly once
        _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<ResidentNote>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}


