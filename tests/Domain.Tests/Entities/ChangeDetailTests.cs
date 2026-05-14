// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Tests.Entities;

/// <summary>
/// Unit tests for the <see cref="ChangeDetail"/> domain entity.
/// </summary>
/// <remarks>
/// Verifies default values and the <see cref="IEntity"/> contract for the field-level
/// change records used by UC-009 (View History and Traceability).
/// </remarks>
public class ChangeDetailTests
{
    #region Default Value Tests

    [Fact]
    public void Constructor_Default_SetsFieldToEmptyString()
    {
        // Arrange + Act
        ChangeDetail detail = new();

        // Assert
        Assert.Equal(string.Empty, detail.Field);
    }

    [Fact]
    public void Constructor_Default_SetsOldValueToEmptyString()
    {
        // Arrange + Act
        ChangeDetail detail = new();

        // Assert
        Assert.Equal(string.Empty, detail.OldValue);
    }

    [Fact]
    public void Constructor_Default_SetsNewValueToEmptyString()
    {
        // Arrange + Act
        ChangeDetail detail = new();

        // Assert
        Assert.Equal(string.Empty, detail.NewValue);
    }

    #endregion

    #region IEntity Contract Tests

    [Fact]
    public void ChangeDetail_ImplementsIEntity_HasIdProperty()
    {
        // Arrange
        Guid expected = Guid.NewGuid();
        ChangeDetail detail = new() { Id = expected };

        // Act
        IEntity asEntity = detail;

        // Assert
        Assert.Equal(expected, asEntity.Id);
    }

    #endregion

    #region Property Assignment Tests

    [Theory]
    [InlineData("Initials", "AB", "CD")]
    [InlineData("TrafficLightStatus", "Green", "Yellow")]
    [InlineData("Note", "Old text", "Updated text")]
    public void Properties_WhenAssigned_ReturnAssignedValues(string field, string oldValue, string newValue)
    {
        // Arrange
        ChangeDetail detail = new()
        {
            // Act
            Field = field,
            OldValue = oldValue,
            NewValue = newValue
        };

        // Assert
        Assert.Equal(field, detail.Field);
        Assert.Equal(oldValue, detail.OldValue);
        Assert.Equal(newValue, detail.NewValue);
    }

    [Fact]
    public void AuditEntryId_WhenAssigned_ReturnsAssignedValue()
    {
        // Arrange
        ChangeDetail detail = new();
        Guid expected = Guid.NewGuid();

        // Act
        detail.AuditEntryId = expected;

        // Assert
        Assert.Equal(expected, detail.AuditEntryId);
    }

    #endregion
}
