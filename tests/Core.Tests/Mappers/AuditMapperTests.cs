// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Audit;
using Core.Mappers;

using Domain.Entities;

namespace Core.Tests.Mappers;

/// <summary>
/// Unit tests for the <see cref="AuditMapper"/> static class.
/// </summary>
/// <remarks>
/// Verifies entity-to-DTO conversion and the metadata parsing used by UC-009
/// (View History and Traceability).
/// </remarks>
public class AuditMapperTests
{
    #region ToDto - Property Mapping Tests

    [Fact]
    public void ToDto_AnyEntry_MapsIdToDto()
    {
        Guid expected = Guid.NewGuid();
        AuditEntry entry = new() { Id = expected, Metadata = "Resident - Added" };

        AuditEntryDto result = AuditMapper.ToDto(entry);

        Assert.Equal(expected, result.Id);
    }

    [Fact]
    public void ToDto_AnyEntry_MapsStartTimeUtcToEventTimeUtc()
    {
        DateTime expected = new(2026, 5, 9, 10, 0, 0, DateTimeKind.Utc);
        AuditEntry entry = new() { StartTimeUtc = expected, Metadata = "Resident - Added" };

        AuditEntryDto result = AuditMapper.ToDto(entry);

        Assert.Equal(expected, result.EventTimeUtc);
    }

    [Fact]
    public void ToDto_AnyEntry_MapsEndTimeUtcToRegisteredTimeUtc()
    {
        DateTime expected = new(2026, 5, 9, 10, 0, 5, DateTimeKind.Utc);
        AuditEntry entry = new() { EndTimeUtc = expected, Metadata = "Resident - Added" };

        AuditEntryDto result = AuditMapper.ToDto(entry);

        Assert.Equal(expected, result.RegisteredTimeUtc);
    }

    [Fact]
    public void ToDto_AnyEntry_MapsMetadataToDescription()
    {
        const string expected = "Resident - Added";
        AuditEntry entry = new() { Metadata = expected };

        AuditEntryDto result = AuditMapper.ToDto(entry);

        Assert.Equal(expected, result.Description);
    }

    [Fact]
    public void ToDto_AnyEntry_MapsUserId()
    {
        Guid expected = Guid.NewGuid();
        AuditEntry entry = new() { UserId = expected, Metadata = "Resident - Added" };

        AuditEntryDto result = AuditMapper.ToDto(entry);

        Assert.Equal(expected, result.UserId);
    }

    [Fact]
    public void ToDto_WithUserName_AssignsUserNameToDto()
    {
        const string expected = "PederRasmussen";
        AuditEntry entry = new() { Metadata = "Resident - Added" };

        AuditEntryDto result = AuditMapper.ToDto(entry, expected);

        Assert.Equal(expected, result.UserName);
    }

    [Fact]
    public void ToDto_WithoutUserName_AssignsEmptyUserName()
    {
        AuditEntry entry = new() { Metadata = "Resident - Added" };

        AuditEntryDto result = AuditMapper.ToDto(entry);

        Assert.Equal(string.Empty, result.UserName);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ToDto_AnyEntry_MapsSucceededFlag(bool succeeded)
    {
        AuditEntry entry = new() { Succeeded = succeeded, Metadata = "Resident - Added" };

        AuditEntryDto result = AuditMapper.ToDto(entry);

        Assert.Equal(succeeded, result.Succeeded);
    }

    #endregion

    #region ToDto - Metadata Parsing Tests

    [Theory]
    [InlineData("Resident - Added", "Resident", "Added")]
    [InlineData("ResidentNote - Modified", "ResidentNote", "Modified")]
    [InlineData("PhoneAssignment - Deleted", "PhoneAssignment", "Deleted")]
    public void ToDto_WithStandardMetadata_SplitsIntoEntityAndChangeType(
        string metadata,
        string expectedEntity,
        string expectedChangeType)
    {
        AuditEntry entry = new() { Metadata = metadata };

        AuditEntryDto result = AuditMapper.ToDto(entry);

        Assert.Equal(expectedEntity, result.Entity);
        Assert.Equal(expectedChangeType, result.ChangeType);
    }

    [Fact]
    public void ToDto_WithMetadataWithoutSeparator_PutsWholeStringAsEntity()
    {
        AuditEntry entry = new() { Metadata = "InvalidFormat" };

        AuditEntryDto result = AuditMapper.ToDto(entry);

        Assert.Equal("InvalidFormat", result.Entity);
        Assert.Equal(string.Empty, result.ChangeType);
    }

    [Fact]
    public void ToDto_WithEmptyMetadata_ReturnsEmptyEntityAndChangeType()
    {
        AuditEntry entry = new() { Metadata = string.Empty };

        AuditEntryDto result = AuditMapper.ToDto(entry);

        Assert.Equal(string.Empty, result.Entity);
        Assert.Equal(string.Empty, result.ChangeType);
    }

    [Fact]
    public void ToDto_WithWhitespaceMetadata_ReturnsEmptyEntityAndChangeType()
    {
        AuditEntry entry = new() { Metadata = "   " };

        AuditEntryDto result = AuditMapper.ToDto(entry);

        Assert.Equal(string.Empty, result.Entity);
        Assert.Equal(string.Empty, result.ChangeType);
    }

    [Fact]
    public void ToDto_WithExtraWhitespaceAroundSeparator_TrimsBothParts()
    {
        AuditEntry entry = new() { Metadata = "  Resident   -   Added  " };

        AuditEntryDto result = AuditMapper.ToDto(entry);

        Assert.Equal("Resident", result.Entity);
        Assert.Equal("Added", result.ChangeType);
    }

    #endregion

    #region ToDto - ChangeDetails Tests

    [Fact]
    public void ToDto_WithChangeDetails_MapsAllDetails()
    {
        AuditEntry entry = new()
        {
            Id = Guid.NewGuid(),
            Metadata = "Resident - Modified",
            ChangeDetails =
            [
                new ChangeDetail { Id = Guid.NewGuid(), Field = "Initials", OldValue = "AB", NewValue = "CD" },
                new ChangeDetail { Id = Guid.NewGuid(), Field = "Note", OldValue = "Old", NewValue = "New" }
            ]
        };

        AuditEntryDto result = AuditMapper.ToDto(entry);

        Assert.Equal(2, result.ChangeDetails.Count());
    }

    [Fact]
    public void ToDto_WithoutChangeDetails_ReturnsEmptyChangeDetails()
    {
        AuditEntry entry = new() { Metadata = "Resident - Added" };

        AuditEntryDto result = AuditMapper.ToDto(entry);

        Assert.Empty(result.ChangeDetails);
    }

    #endregion

    #region ToDtos Tests

    [Fact]
    public void ToDtos_WithMultipleEntries_MapsAll()
    {
        List<AuditEntry> entries =
        [
            new AuditEntry { Id = Guid.NewGuid(), Metadata = "Resident - Added" },
            new AuditEntry { Id = Guid.NewGuid(), Metadata = "ResidentNote - Modified" },
            new AuditEntry { Id = Guid.NewGuid(), Metadata = "PhoneAssignment - Deleted" }
        ];

        IEnumerable<AuditEntryDto> result = AuditMapper.ToDtos(entries);

        Assert.Equal(3, result.Count());
    }

    [Fact]
    public void ToDtos_WithEmptyCollection_ReturnsEmptyCollection()
    {
        IEnumerable<AuditEntry> entries = [];

        IEnumerable<AuditEntryDto> result = AuditMapper.ToDtos(entries);

        Assert.Empty(result);
    }

    [Fact]
    public void ToDtos_WithMultipleEntries_AssignsEmptyUserNameToEach()
    {
        List<AuditEntry> entries =
        [
            new AuditEntry { Id = Guid.NewGuid(), Metadata = "Resident - Added" },
            new AuditEntry { Id = Guid.NewGuid(), Metadata = "Resident - Modified" }
        ];

        IEnumerable<AuditEntryDto> result = AuditMapper.ToDtos(entries);

        Assert.All(result, dto => Assert.Equal(string.Empty, dto.UserName));
    }

    #endregion
}
