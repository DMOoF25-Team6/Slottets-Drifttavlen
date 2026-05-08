// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Api.Controllers;

using Core.DTOs;
using Core.Interfaces.Repositories;

using Domain.Entities;
using Domain.Enums;

using Microsoft.AspNetCore.Mvc;

using Moq;

namespace WebApi.Tests.Controllers;

public class ResidentControllerTests
{
    private readonly Mock<IResidentRepository> _mockRepo;
    private readonly ResidentController _controller;

    public ResidentControllerTests()
    {
        _mockRepo = new Mock<IResidentRepository>(MockBehavior.Strict);
        _controller = new ResidentController(_mockRepo.Object);
    }

    #region Functionality
    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetAll_WhenResidentsExist_ReturnsAllResidents()
    {
        // Arrange
        List<Resident> residents =
        [
                new Resident { Id = Guid.NewGuid(), Initials = "AB", TrafficLightStatus = (TrafficLightStatus)1, Notes = [] },
                new Resident { Id = Guid.NewGuid(), Initials = "CD", TrafficLightStatus = (TrafficLightStatus)2, Notes = [] }
            ];
        _ = _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(residents);

        // Act
        ActionResult<IEnumerable<ResidentResponseDto>> result = await _controller.GetAll(CancellationToken.None);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
        IEnumerable<ResidentResponseDto> value = Assert.IsAssignableFrom<IEnumerable<ResidentResponseDto>>(okResult.Value);
        Assert.Equal(2, value.Count());
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetById_WhenResidentExists_ReturnsResident()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        Resident resident = new() { Id = residentId, Initials = "EF", TrafficLightStatus = TrafficLightStatus.Green, Notes = [] };
        _ = _mockRepo.Setup(r => r.GetByIdAsync(residentId, It.IsAny<CancellationToken>())).ReturnsAsync(resident);

        // Act
        ActionResult<ResidentResponseDto> result = await _controller.GetById(residentId, CancellationToken.None);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
        ResidentResponseDto value = Assert.IsType<ResidentResponseDto>(okResult.Value);
        Assert.Equal(residentId, value.Id);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task Create_ValidDto_ReturnsCreatedResident()
    {
        // Arrange
        ResidentCreateRequestDto dto = new() { Initials = "GH", FirstName = "G", LastName = "H", TrafficLightStatus = TrafficLightStatus.Green };
        Resident created = new() { Id = Guid.NewGuid(), Initials = dto.Initials, FirstName = dto.FirstName, LastName = dto.LastName, TrafficLightStatus = dto.TrafficLightStatus, Notes = [] };
        _ = _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Resident>(), It.IsAny<CancellationToken>())).ReturnsAsync(created);

        // Act
        ActionResult<ResidentResponseDto> result = await _controller.Create(dto, CancellationToken.None);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
        ResidentResponseDto value = Assert.IsType<ResidentResponseDto>(okResult.Value);
        Assert.Equal(created.Id, value.Id);
        Assert.Equal(dto.Initials, value.Initials);
    }
    #endregion

    #region EdgeCase
    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task GetById_WhenResidentDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        _ = _mockRepo.Setup(r => r.GetByIdAsync(residentId, It.IsAny<CancellationToken>())).ReturnsAsync((Resident?)null);

        // Act
        ActionResult<ResidentResponseDto> result = await _controller.GetById(residentId, CancellationToken.None);

        // Assert
        _ = Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task Create_NullDto_ReturnsBadRequest()
    {
        // Act
        ResidentCreateRequestDto? nullDto = null;
        ActionResult<ResidentResponseDto> result = await _controller.Create(nullDto!, CancellationToken.None);

        // Assert
        BadRequestObjectResult badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
        ErrorDto error = Assert.IsType<ErrorDto>(badRequest.Value);
        Assert.NotNull(error.ErrorMessages);
        Assert.Contains("Request body cannot be null.", error.ErrorMessages!);
    }
    #endregion
}
