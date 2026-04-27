// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Managers;
using Core.Services;

using Moq;

namespace Core.Tests.Services;

/// <summary>
/// Unit tests for <see cref="PhoneAssignmentService"/>.
/// </summary>
public class PhoneAssignmentServiceTests
{
    #region Fields

    private readonly Mock<IPhoneAssignmentManager> _phoneAssignmentManagerMock;
    private readonly PhoneAssignmentService _service;

    #endregion

    #region Constructor

    public PhoneAssignmentServiceTests()
    {
        _phoneAssignmentManagerMock = new Mock<IPhoneAssignmentManager>();
        _service = new PhoneAssignmentService(_phoneAssignmentManagerMock.Object);
    }

    #endregion

    #region GetCurrentPhoneAssignmentsForActiveShift

    [Fact]
    public async Task GetCurrentPhoneAssignmentsForActiveShift_ValidRequest_CallsManagerOnce()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;

        _ = _phoneAssignmentManagerMock
            .Setup(m => m.GetCurrentPhoneAssignmentsForActiveShift(cancellationToken))
            .ReturnsAsync([]);

        // Act
        _ = await _service.GetCurrentPhoneAssignmentsForActiveShiftAsync(cancellationToken);

        // Assert
        _phoneAssignmentManagerMock.Verify(
            m => m.GetCurrentPhoneAssignmentsForActiveShift(cancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task GetCurrentPhoneAssignmentsForActiveShift_ValidRequest_ReturnsManagerResult()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;

        IEnumerable<PhoneAssignmentDto> expected =
        [
            new PhoneAssignmentDto { PhoneNumber = "41522", ShiftType = "Day" },
            new PhoneAssignmentDto { PhoneNumber = "41523", ShiftType = "Day" }
        ];

        _ = _phoneAssignmentManagerMock
            .Setup(m => m.GetCurrentPhoneAssignmentsForActiveShift(cancellationToken))
            .ReturnsAsync(expected);

        // Act
        IEnumerable<PhoneAssignmentDto> result =
            await _service.GetCurrentPhoneAssignmentsForActiveShiftAsync(cancellationToken);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetCurrentPhoneAssignmentsForActiveShift_EmptyResult_ReturnsEmptyCollection()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;

        _ = _phoneAssignmentManagerMock
            .Setup(m => m.GetCurrentPhoneAssignmentsForActiveShift(cancellationToken))
            .ReturnsAsync([]);

        // Act
        IEnumerable<PhoneAssignmentDto> result =
            await _service.GetCurrentPhoneAssignmentsForActiveShiftAsync(cancellationToken);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetCurrentPhoneAssignmentsForActiveShift_CancellationRequested_PropagatesCancellationToken()
    {
        // Arrange
        CancellationToken cancellationToken = new(canceled: true);

        _ = _phoneAssignmentManagerMock
            .Setup(m => m.GetCurrentPhoneAssignmentsForActiveShift(cancellationToken))
            .ThrowsAsync(new OperationCanceledException(cancellationToken));

        // Act & Assert
        _ = await Assert.ThrowsAsync<OperationCanceledException>(
            () => _service.GetCurrentPhoneAssignmentsForActiveShiftAsync(cancellationToken));
    }

    #endregion
}
