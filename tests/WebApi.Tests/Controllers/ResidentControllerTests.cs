// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Security.Claims;

using Api.Controllers;

using Core.DTOs;
using Core.Interfaces.Repositories;

using Domain.Entities;
using Domain.Enums;

using Microsoft.AspNetCore.Http;
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

    private void SetUserDepartmentClaim(Department? department)
    {
        Claim[] claims = department.HasValue
            ? [new("Department", department.Value.ToString())]
            : [];

        ClaimsPrincipal principal = new(new ClaimsIdentity(claims, "TestAuth"));
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = principal
            }
        };
    }

    #region Functionality
    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetAll_WhenResidentsExist_ReturnsAllResidents()
    {
        // Arrange
        List<Resident> residents =
        [
                new Resident { Id = Guid.NewGuid(), Initials = "AB", TrafficLightStatus = (TrafficLightStatus)1, Department = Department.Slottet, Notes = [] },
                new Resident { Id = Guid.NewGuid(), Initials = "CD", TrafficLightStatus = (TrafficLightStatus)2, Department = Department.Skoven, Notes = [] }
            ];
        _ = _mockRepo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(residents);

        // Act
        ActionResult<IEnumerable<ResidentResponseDto>> result = await _controller.GetAll(CancellationToken.None);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
        IEnumerable<ResidentResponseDto> value = Assert.IsType<IEnumerable<ResidentResponseDto>>(okResult.Value, exactMatch: false);
        ResidentResponseDto[] residentDtos = [.. value];
        Assert.Equal(2, residentDtos.Length);
        Assert.Equal(Department.Slottet, residentDtos[0].Department);
        Assert.Equal(Department.Skoven, residentDtos[1].Department);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetById_WhenResidentExists_ReturnsResident()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        Resident resident = new() { Id = residentId, Initials = "EF", TrafficLightStatus = TrafficLightStatus.Green, Department = Department.Marken, Notes = [] };
        _ = _mockRepo.Setup(r => r.GetByIdAsync(residentId, It.IsAny<CancellationToken>())).ReturnsAsync(resident);

        // Act
        ActionResult<ResidentResponseDto> result = await _controller.GetById(residentId, CancellationToken.None);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
        ResidentResponseDto value = Assert.IsType<ResidentResponseDto>(okResult.Value);
        Assert.Equal(residentId, value.Id);
        Assert.Equal(Department.Marken, value.Department);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task Create_ValidDto_ReturnsCreatedResident()
    {
        // Arrange
        SetUserDepartmentClaim(null);
        ResidentCreateRequestDto dto = new() { Initials = "GH", FirstName = "G", LastName = "H", TrafficLightStatus = TrafficLightStatus.Green, Department = Department.Slottet };
        Resident created = new() { Id = Guid.NewGuid(), Initials = dto.Initials, FirstName = dto.FirstName, LastName = dto.LastName, TrafficLightStatus = dto.TrafficLightStatus, Department = dto.Department, Notes = [] };
        _ = _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Resident>(), It.IsAny<CancellationToken>())).ReturnsAsync(created);

        // Act
        ActionResult<ResidentResponseDto> result = await _controller.Create(dto, CancellationToken.None);

        // Assert
        OkObjectResult okResult = Assert.IsType<OkObjectResult>(result.Result);
        ResidentResponseDto value = Assert.IsType<ResidentResponseDto>(okResult.Value);
        Assert.Equal(created.Id, value.Id);
        Assert.Equal(dto.Initials, value.Initials);
        Assert.Equal(Department.Slottet, value.Department);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task Update_WhenResidentExistsAndDepartmentMatches_ReturnsNoContent()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        SetUserDepartmentClaim(Department.Slottet);
        Resident existing = new()
        {
            Id = residentId,
            Initials = "AB",
            FirstName = "A",
            LastName = "B",
            Department = Department.Slottet,
            Notes = []
        };
        ResidentUpdateRequestDto dto = new()
        {
            Initials = "CD",
            FirstName = "C",
            LastName = "D",
            TrafficLightStatus = TrafficLightStatus.Yellow,
            Department = Department.Slottet
        };
        _ = _mockRepo.Setup(r => r.GetByIdAsync(residentId, It.IsAny<CancellationToken>())).ReturnsAsync(existing);
        _ = _mockRepo.Setup(r => r.UpdateAsync(existing, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        ActionResult result = await _controller.Update(residentId, dto, CancellationToken.None);

        // Assert
        _ = Assert.IsType<NoContentResult>(result);
        Assert.Equal(dto.Initials, existing.Initials);
        Assert.Equal(dto.FirstName, existing.FirstName);
        Assert.Equal(dto.LastName, existing.LastName);
        Assert.Equal(dto.TrafficLightStatus, existing.TrafficLightStatus);
        Assert.Equal(dto.Department, existing.Department);
        _mockRepo.Verify(r => r.UpdateAsync(existing, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task Delete_WhenResidentExistsAndDepartmentMatches_ReturnsNoContent()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        SetUserDepartmentClaim(Department.Skoven);
        Resident existing = new()
        {
            Id = residentId,
            Initials = "AB",
            Department = Department.Skoven,
            Notes = []
        };
        _ = _mockRepo.Setup(r => r.GetByIdAsync(residentId, It.IsAny<CancellationToken>())).ReturnsAsync(existing);
        _ = _mockRepo.Setup(r => r.DeleteAsync(existing, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        // Act
        ActionResult result = await _controller.Delete(residentId, CancellationToken.None);

        // Assert
        _ = Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(r => r.DeleteAsync(existing, It.IsAny<CancellationToken>()), Times.Once);
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

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task Create_WhenUserDepartmentDoesNotMatchRequestDepartment_ReturnsForbid()
    {
        // Arrange
        SetUserDepartmentClaim(Department.Slottet);
        ResidentCreateRequestDto dto = new()
        {
            Initials = "GH",
            FirstName = "G",
            LastName = "H",
            TrafficLightStatus = TrafficLightStatus.Green,
            Department = Department.Skoven
        };

        // Act
        ActionResult<ResidentResponseDto> result = await _controller.Create(dto, CancellationToken.None);

        // Assert
        _ = Assert.IsType<ForbidResult>(result.Result);
        _mockRepo.Verify(r => r.CreateAsync(It.IsAny<Resident>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task Update_WhenResidentDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        ResidentUpdateRequestDto dto = new()
        {
            Initials = "CD",
            FirstName = "C",
            LastName = "D",
            TrafficLightStatus = TrafficLightStatus.Green,
            Department = Department.Slottet
        };
        _ = _mockRepo.Setup(r => r.GetByIdAsync(residentId, It.IsAny<CancellationToken>())).ReturnsAsync((Resident?)null);

        // Act
        ActionResult result = await _controller.Update(residentId, dto, CancellationToken.None);

        // Assert
        _ = Assert.IsType<NotFoundResult>(result);
        _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Resident>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task Update_WhenUserDepartmentDoesNotMatchResident_ReturnsForbid()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        SetUserDepartmentClaim(Department.Slottet);
        Resident existing = new()
        {
            Id = residentId,
            Department = Department.Skoven,
            Notes = []
        };
        ResidentUpdateRequestDto dto = new()
        {
            Initials = "CD",
            FirstName = "C",
            LastName = "D",
            TrafficLightStatus = TrafficLightStatus.Green,
            Department = Department.Skoven
        };
        _ = _mockRepo.Setup(r => r.GetByIdAsync(residentId, It.IsAny<CancellationToken>())).ReturnsAsync(existing);

        // Act
        ActionResult result = await _controller.Update(residentId, dto, CancellationToken.None);

        // Assert
        _ = Assert.IsType<ForbidResult>(result);
        _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Resident>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task Update_WhenUserCannotManageTargetDepartment_ReturnsForbid()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        SetUserDepartmentClaim(Department.Slottet);
        Resident existing = new()
        {
            Id = residentId,
            Department = Department.Slottet,
            Notes = []
        };
        ResidentUpdateRequestDto dto = new()
        {
            Initials = "CD",
            FirstName = "C",
            LastName = "D",
            TrafficLightStatus = TrafficLightStatus.Green,
            Department = Department.Skoven
        };
        _ = _mockRepo.Setup(r => r.GetByIdAsync(residentId, It.IsAny<CancellationToken>())).ReturnsAsync(existing);

        // Act
        ActionResult result = await _controller.Update(residentId, dto, CancellationToken.None);

        // Assert
        _ = Assert.IsType<ForbidResult>(result);
        _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Resident>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task Delete_WhenResidentDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        _ = _mockRepo.Setup(r => r.GetByIdAsync(residentId, It.IsAny<CancellationToken>())).ReturnsAsync((Resident?)null);

        // Act
        ActionResult result = await _controller.Delete(residentId, CancellationToken.None);

        // Assert
        _ = Assert.IsType<NotFoundResult>(result);
        _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Resident>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task Delete_WhenUserDepartmentDoesNotMatchResident_ReturnsForbid()
    {
        // Arrange
        Guid residentId = Guid.NewGuid();
        SetUserDepartmentClaim(Department.Marken);
        Resident existing = new()
        {
            Id = residentId,
            Department = Department.Skoven,
            Notes = []
        };
        _ = _mockRepo.Setup(r => r.GetByIdAsync(residentId, It.IsAny<CancellationToken>())).ReturnsAsync(existing);

        // Act
        ActionResult result = await _controller.Delete(residentId, CancellationToken.None);

        // Assert
        _ = Assert.IsType<ForbidResult>(result);
        _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Resident>(), It.IsAny<CancellationToken>()), Times.Never);
    }
    #endregion
}
