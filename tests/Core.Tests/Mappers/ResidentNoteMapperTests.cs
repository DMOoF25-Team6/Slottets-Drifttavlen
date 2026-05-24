// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Mappers;

using Domain.Entities;

namespace Core.Tests.Mappers;

public class ResidentNoteMapperTests
{
    [Fact]
    [Trait("Category", "Functionality")]
    public void ToDto_MapsFields()
    {
        DateTime edited = new(2026, 5, 9, 10, 0, 0, DateTimeKind.Utc);
        ResidentNote entity = new() { Id = Guid.NewGuid(), Note = "obs", EditedAt = edited, ResidentId = Guid.NewGuid() };

        ResidentNoteDto dto = ResidentNoteMapper.ToDto(entity);

        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal("obs", dto.Note);
        Assert.Equal(edited, dto.Timestamp);
        Assert.Equal(string.Empty, dto.Initials);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public void ToDto_NullEntity_Throws() =>
        Assert.Throws<ArgumentNullException>(() => ResidentNoteMapper.ToDto(null!));

    [Fact]
    [Trait("Category", "Functionality")]
    public void ToDtos_MapsCollection()
    {
        ResidentNote[] entities = [new() { Note = "a" }, new() { Note = "b" }];

        List<ResidentNoteDto> dtos = ResidentNoteMapper.ToDtos(entities).ToList();

        Assert.Equal(2, dtos.Count);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public void ToDtos_Null_Throws() =>
        Assert.Throws<ArgumentNullException>(() => ResidentNoteMapper.ToDtos(null!).ToList());

    [Fact]
    [Trait("Category", "Functionality")]
    public void ToNewEntity_SetsFieldsAndGeneratesId()
    {
        Guid residentId = Guid.NewGuid();

        ResidentNote entity = ResidentNoteMapper.ToNewEntity(residentId, "note");

        Assert.NotEqual(Guid.Empty, entity.Id);
        Assert.Equal(residentId, entity.ResidentId);
        Assert.Equal("note", entity.Note);
    }

    [Fact]
    [Trait("Category", "Concurrency")]
    public void ToDto_ParallelMapping_IsThreadSafe()
    {
        ResidentNote entity = new() { Id = Guid.NewGuid(), Note = "x", EditedAt = DateTime.UtcNow };

        ResidentNoteDto[] results = Enumerable.Range(0, 200).AsParallel().Select(_ => ResidentNoteMapper.ToDto(entity)).ToArray();

        Assert.All(results, r => Assert.Equal(entity.Id, r.Id));
    }
}
