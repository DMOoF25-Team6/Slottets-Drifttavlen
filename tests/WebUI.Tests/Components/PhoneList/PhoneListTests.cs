// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Bunit;

using Core.DTOs;
using Core.Interfaces.Managers;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using PhoneListComponent = WebUI.Components.PhoneList.PhoneList;

using Xunit;

namespace WebUI.Tests.Components.PhoneList;

/// <summary>
/// Unit tests for the <see cref="PhoneList"/> Blazor component.
/// </summary>
public class PhoneListTests : Bunit.TestContext
{
    #region Fields

    private readonly Mock<IPhoneAssignmentManager> _phoneAssignmentManagerMock;

    #endregion

    #region Constructor

    public PhoneListTests()
    {
        _phoneAssignmentManagerMock = new Mock<IPhoneAssignmentManager>();
        Services.AddScoped(_ => _phoneAssignmentManagerMock.Object);
    }

    #endregion

    #region Render Tests

    [Fact]
    public void Render_EmptyAssignments_RendersTable()
    {
        // Arrange
        _ = _phoneAssignmentManagerMock
            .Setup(m => m.GetCurrentPhoneAssignmentsForActiveShift(It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        IRenderedComponent<PhoneListComponent> cut = RenderComponent<PhoneListComponent>();

        // Assert
        cut.WaitForAssertion(() => Assert.NotNull(cut.Find("table")));
    }

    [Fact]
    public void Render_WithAssignment_RendersPhoneNumber()
    {
        // Arrange
        IEnumerable<PhoneAssignmentDto> assignments =
        [
            new PhoneAssignmentDto { PhoneNumber = "41522", ShiftType = "Day", AssignedStaffName = "Anna" }
        ];

        _ = _phoneAssignmentManagerMock
            .Setup(m => m.GetCurrentPhoneAssignmentsForActiveShift(It.IsAny<CancellationToken>()))
            .ReturnsAsync(assignments);

        // Act
        IRenderedComponent<PhoneListComponent> cut = RenderComponent<PhoneListComponent>();

        // Assert
        cut.WaitForAssertion(() => Assert.Contains("41522", cut.Markup));
    }

    [Fact]
    public void Render_WithAssignment_RendersAssignedStaffName()
    {
        // Arrange
        IEnumerable<PhoneAssignmentDto> assignments =
        [
            new PhoneAssignmentDto { PhoneNumber = "41522", ShiftType = "Day", AssignedStaffName = "Anna" }
        ];

        _ = _phoneAssignmentManagerMock
            .Setup(m => m.GetCurrentPhoneAssignmentsForActiveShift(It.IsAny<CancellationToken>()))
            .ReturnsAsync(assignments);

        // Act
        IRenderedComponent<PhoneListComponent> cut = RenderComponent<PhoneListComponent>();

        // Assert
        cut.WaitForAssertion(() => Assert.Contains("Anna", cut.Markup));
    }

    [Fact]
    public void Render_ManagerThrows_RendersErrorMessage()
    {
        // Arrange
        _ = _phoneAssignmentManagerMock
            .Setup(m => m.GetCurrentPhoneAssignmentsForActiveShift(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException("Connection failed"));

        // Act
        IRenderedComponent<PhoneListComponent> cut = RenderComponent<PhoneListComponent>();

        // Assert
        cut.WaitForAssertion(() => Assert.Contains("Kunne ikke hente", cut.Markup));
    }

    [Fact]
    public void Render_Always_RendersShiftBadge()
    {
        // Arrange
        _ = _phoneAssignmentManagerMock
            .Setup(m => m.GetCurrentPhoneAssignmentsForActiveShift(It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        IRenderedComponent<PhoneListComponent> cut = RenderComponent<PhoneListComponent>();

        // Assert
        cut.WaitForAssertion(() => Assert.Contains("Vagt:", cut.Markup));
    }

    #endregion
}
