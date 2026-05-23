// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Mappers;
using Domain.Entities;
using Domain.Enums;
using Xunit;

namespace Core.Tests.Mappers;

/// <summary>
/// Unit tests for <see cref="ResidentMapper"/> (UC-001/014/015/024).
/// </summary>
public class ResidentMapperTests
{
    #region ToResident (ResponseDto)

    [Fact]
    public void ToResident_FromResponseDto_MapsAllFieldsIncludingExtended()
    {
        ResidentResponseDto dto = new()
        {
            Id = Guid.NewGuid(),
            Initials = "AB",
            FirstName = "Anders",
            LastName = "Bjerg",
            TrafficLightStatus = 1,
            Department = Department.Skoven,
            Activity = "Handle",
            Companion = "KP",
            Amount = "300kr",
            Info = "Eget kort"
        };

        Resident result = ResidentMapper.ToResident(dto);

        Assert.Equal(dto.Id, result.Id);
        Assert.Equal("AB", result.Initials);
        Assert.Equal("Anders", result.FirstName);
        Assert.Equal("Bjerg", result.LastName);
        Assert.Equal(TrafficLightStatus.Yellow, result.TrafficLightStatus);
        Assert.Equal(Department.Skoven, result.Department);
        Assert.Equal("Handle", result.Activity);
        Assert.Equal("KP", result.Companion);
        Assert.Equal("300kr", result.Amount);
        Assert.Equal("Eget kort", result.Info);
    }

    [Theory]
    [InlineData(0, TrafficLightStatus.Green)]
    [InlineData(1, TrafficLightStatus.Yellow)]
    [InlineData(2, TrafficLightStatus.Red)]
    public void ToResident_FromResponseDto_MapsTrafficLightInt(int input, TrafficLightStatus expected)
    {
        ResidentResponseDto dto = new() { TrafficLightStatus = input };

        Resident result = ResidentMapper.ToResident(dto);

        Assert.Equal(expected, result.TrafficLightStatus);
    }

    [Fact]
    public void ToResident_FromResponseDto_NullTrafficLight_MapsToNull()
    {
        ResidentResponseDto dto = new() { TrafficLightStatus = null };

        Resident result = ResidentMapper.ToResident(dto);

        Assert.Null(result.TrafficLightStatus);
    }

    #endregion

    #region ToResidentResponseDto (entity)

    [Fact]
    public void ToResidentResponseDto_MapsTrafficLightEnumToInt()
    {
        Resident entity = new()
        {
            Id = Guid.NewGuid(),
            Initials = "CC",
            TrafficLightStatus = TrafficLightStatus.Red,
            Department = Department.Slottet
        };

        ResidentResponseDto dto = ResidentMapper.ToResidentResponseDto(entity);

        Assert.Equal(2, dto.TrafficLightStatus);
        Assert.Equal("CC", dto.Initials);
    }

    [Fact]
    public void ToResidentResponseDto_NullNotes_ReturnsEmptyList()
    {
        Resident entity = new()
        {
            Id = Guid.NewGuid(),
            Initials = "DD",
            Department = Department.Marken,
            Notes = null!
        };

        ResidentResponseDto dto = ResidentMapper.ToResidentResponseDto(entity);

        Assert.NotNull(dto.Notes);
        Assert.Empty(dto.Notes);
    }

    [Fact]
    public void ToResidentResponseDto_MapsNotes()
    {
        Resident entity = new()
        {
            Id = Guid.NewGuid(),
            Initials = "EE",
            Department = Department.Slottet,
            Notes =
            [
                new ResidentNote { Id = Guid.NewGuid(), Note = "Spiste godt", EditedAt = DateTime.UtcNow }
            ]
        };

        ResidentResponseDto dto = ResidentMapper.ToResidentResponseDto(entity);

        Assert.Single(dto.Notes);
        Assert.Equal("Spiste godt", dto.Notes[0].Note);
    }

    #endregion

    #region ToResident (CreateRequestDto)

    [Fact]
    public void ToResident_FromCreateDto_GeneratesIdAndMapsFields()
    {
        ResidentCreateRequestDto dto = new()
        {
            Initials = "FF",
            FirstName = "Frank",
            LastName = "Frandsen",
            TrafficLightStatus = TrafficLightStatus.Green,
            Department = Department.Skoven,
            Activity = "Spadseretur"
        };

        Resident result = ResidentMapper.ToResident(dto);

        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal("Frank", result.FirstName);
        Assert.Equal(TrafficLightStatus.Green, result.TrafficLightStatus);
        Assert.Equal("Spadseretur", result.Activity);
    }

    #endregion

    #region ApplyUpdate

    [Fact]
    public void ApplyUpdate_OverwritesFields()
    {
        Resident entity = new()
        {
            Id = Guid.NewGuid(),
            Initials = "GG",
            FirstName = "Old",
            Department = Department.Slottet
        };
        ResidentUpdateRequestDto dto = new()
        {
            Initials = "HH",
            FirstName = "New",
            LastName = "Name",
            TrafficLightStatus = TrafficLightStatus.Red,
            Department = Department.Marken,
            Activity = "Hvile"
        };

        ResidentMapper.ApplyUpdate(entity, dto);

        Assert.Equal("HH", entity.Initials);
        Assert.Equal("New", entity.FirstName);
        Assert.Equal(Department.Marken, entity.Department);
        Assert.Equal(TrafficLightStatus.Red, entity.TrafficLightStatus);
        Assert.Equal("Hvile", entity.Activity);
    }

    [Fact]
    public void ApplyUpdate_NullEntity_Throws()
    {
        ResidentUpdateRequestDto dto = new() { Initials = "x", FirstName = "y", LastName = "z", TrafficLightStatus = null };
        Assert.Throws<ArgumentNullException>(() => ResidentMapper.ApplyUpdate(null!, dto));
    }

    [Fact]
    public void ApplyUpdate_NullDto_Throws()
    {
        Resident entity = new() { Initials = "x" };
        Assert.Throws<ArgumentNullException>(() => ResidentMapper.ApplyUpdate(entity, null!));
    }

    #endregion

    #region Note round-trip

    [Fact]
    public void ResidentNote_RoundTrip_PreservesValues()
    {
        ResidentNote note = new()
        {
            Id = Guid.NewGuid(),
            Note = "Test note",
            EditedAt = new DateTime(2026, 5, 23, 12, 0, 0, DateTimeKind.Utc)
        };

        ResidentNoteDto dto = ResidentMapper.ToResidentNoteDto(note);
        ResidentNote back = ResidentMapper.ToResidentNote(dto);

        Assert.Equal(note.Id, back.Id);
        Assert.Equal(note.Note, back.Note);
        Assert.Equal(note.EditedAt, back.EditedAt);
    }

    #endregion
}
