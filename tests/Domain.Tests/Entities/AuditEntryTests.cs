// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Tests.Entities;

/// <summary>
/// Unit tests for the <see cref="AuditEntry"/> domain entity.
/// </summary>
/// <remarks>
/// Verifies default values, the <see cref="IEntity"/> contract, and the relationship
/// to <see cref="ChangeDetail"/> records used by UC-009 (View History and Traceability).
/// </remarks>
public class AuditEntryTests
{
    #region Default Value Tests

    [Fact]
    public void Constructor_Default_SetsMetadataToEmptyString()
    {
        // Arrange + Act
        AuditEntry entry = new();

        // Assert
        Assert.Equal(string.Empty, entry.Metadata);
    }

    [Fact]
    public void Constructor_Default_SetsErrorMessageToEmptyString()
    {
        // Arrange + Act
        AuditEntry entry = new();

        // Assert
        Assert.Equal(string.Empty, entry.ErrorMessage);
    }

    [Fact]
    public void Constructor_Default_SetsChangeDetailsToEmptyCollection()
    {
        // Arrange + Act
        AuditEntry entry = new();

        // Assert
        Assert.NotNull(entry.ChangeDetails);
        Assert.Empty(entry.ChangeDetails);
    }

    [Fact]
    public void Constructor_Default_SetsSucceededToFalse()
    {
        // Arrange + Act
        AuditEntry entry = new();

        // Assert
        Assert.False(entry.Succeeded);
    }

    #endregion

    #region IEntity Contract Tests

    [Fact]
    public void AuditEntry_ImplementsIEntity_HasIdProperty()
    {
        // Arrange
        Guid expected = Guid.NewGuid();
        AuditEntry entry = new() { Id = expected };

        // Act
        IEntity asEntity = entry;

        // Assert
        Assert.Equal(expected, asEntity.Id);
    }

    #endregion

    #region Property Assignment Tests

    [Fact]
    public void Metadata_WhenAssigned_ReturnsAssignedValue()
    {
        // Arrange
        AuditEntry entry = new();
        const string expected = "Resident - Added";

        // Act
        entry.Metadata = expected;

        // Assert
        Assert.Equal(expected, entry.Metadata);
    }

    [Fact]
    public void StartTimeUtc_WhenAssigned_ReturnsAssignedValue()
    {
        // Arrange
        AuditEntry entry = new();
        DateTime expected = new(2026, 5, 9, 10, 30, 0, DateTimeKind.Utc);

        // Act
        entry.StartTimeUtc = expected;

        // Assert
        Assert.Equal(expected, entry.StartTimeUtc);
    }

    [Fact]
    public void EndTimeUtc_WhenAssigned_ReturnsAssignedValue()
    {
        // Arrange
        AuditEntry entry = new();
        DateTime expected = new(2026, 5, 9, 10, 30, 5, DateTimeKind.Utc);

        // Act
        entry.EndTimeUtc = expected;

        // Assert
        Assert.Equal(expected, entry.EndTimeUtc);
    }

    [Fact]
    public void UserId_WhenAssigned_ReturnsAssignedValue()
    {
        // Arrange
        AuditEntry entry = new();
        Guid expected = Guid.NewGuid();

        // Act
        entry.UserId = expected;

        // Assert
        Assert.Equal(expected, entry.UserId);
    }

    #endregion

    #region Relationship Tests

    [Fact]
    public void ChangeDetails_WhenChangeDetailAdded_ContainsThatDetail()
    {
        // Arrange
        AuditEntry entry = new() { Id = Guid.NewGuid() };
        ChangeDetail detail = new()
        {
            Id = Guid.NewGuid(),
            Field = "Initials",
            OldValue = "AB",
            NewValue = "CD",
            AuditEntryId = entry.Id
        };

        // Act
        entry.ChangeDetails.Add(detail);

        // Assert
        Assert.Single(entry.ChangeDetails);
        Assert.Contains(detail, entry.ChangeDetails);
    }

    [Fact]
    public void ChangeDetails_WhenMultipleDetailsAdded_ContainsAll()
    {
        // Arrange
        AuditEntry entry = new() { Id = Guid.NewGuid() };
        ChangeDetail first = new() { Id = Guid.NewGuid(), Field = "Initials", AuditEntryId = entry.Id };
        ChangeDetail second = new() { Id = Guid.NewGuid(), Field = "TrafficLightStatus", AuditEntryId = entry.Id };

        // Act
        entry.ChangeDetails.Add(first);
        entry.ChangeDetails.Add(second);

        // Assert
        Assert.Equal(2, entry.ChangeDetails.Count);
    }

    #endregion
}
