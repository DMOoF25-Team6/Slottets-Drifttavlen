// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;
using Core.Services;

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
        var residentId = Guid.NewGuid();
        var notes = new List<ResidentNote>
        {
            new ResidentNote { Id = Guid.NewGuid(), ResidentId = residentId, Note = "Note 1" },
            new ResidentNote { Id = Guid.NewGuid(), ResidentId = residentId, Note = "Note 2" }
        };
        _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
          .ReturnsAsync(notes);
        // Act
        var result = await _service.GetAllByResidentIdAsync(residentId, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, note => Assert.Equal(residentId, note.ResidentId));
    }

    [Fact]
    public async Task AddAsync_ReturnsTrueWhenNoteAdded()
    {
        // Arrange
        var residentId = Guid.NewGuid();
        _mockRepo.Setup(r => r.AddAsync(It.IsAny<ResidentNote>(), It.IsAny<CancellationToken>()))
         .ReturnsAsync(new ResidentNote());

        // Act
        var result = await _service.AddAsync(residentId, "New Note", TestContext.Current.CancellationToken);

        // Assert
        Assert.True(result);
    }
}


