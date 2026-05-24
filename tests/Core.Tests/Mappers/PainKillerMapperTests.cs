// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Mappers;

using Domain.Entities;

namespace Core.Tests.Mappers;

public class PainKillerMapperTests
{
    [Fact]
    [Trait("Category", "Functionality")]
    public void ToPainkillerStatusDto_WithRecords_MapsTypesAndMaxNextAllowed()
    {
        Guid residentId = Guid.NewGuid();
        DateTime later = new(2026, 5, 1, 18, 0, 0, DateTimeKind.Utc);
        PainkillerRecord[] records =
        [
            new() { Type = "Paracetamol", NextAllowedTime = new DateTime(2026, 5, 1, 12, 0, 0, DateTimeKind.Utc) },
            new() { Type = "Ibuprofen", NextAllowedTime = later }
        ];

        PainkillerStatusDto dto = PainKillerMapper.ToPainkillerStatusDto(residentId, records);

        Assert.Equal(residentId, dto.ResidentId);
        Assert.Equal(2, dto.Types.Count());
        Assert.Equal(later, dto.NextAllowedTime);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public void ToPainkillerStatusDto_EmptyRecords_UsesNowAndEmptyTypes()
    {
        DateTime before = DateTime.UtcNow;

        PainkillerStatusDto dto = PainKillerMapper.ToPainkillerStatusDto(Guid.NewGuid(), []);

        Assert.Empty(dto.Types);
        Assert.InRange(dto.NextAllowedTime, before.AddMinutes(-1), DateTime.UtcNow.AddMinutes(1));
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public void ToPainkillerStatusDto_NullRecords_Throws() =>
        Assert.Throws<ArgumentNullException>(() => PainKillerMapper.ToPainkillerStatusDto(Guid.NewGuid(), null!));

    [Fact]
    [Trait("Category", "Concurrency")]
    public void ToPainkillerStatusDto_ParallelMapping_IsThreadSafe()
    {
        Guid residentId = Guid.NewGuid();
        PainkillerRecord[] records = [new() { Type = "Paracetamol", NextAllowedTime = DateTime.UtcNow }];

        PainkillerStatusDto[] results = Enumerable.Range(0, 200).AsParallel()
            .Select(_ => PainKillerMapper.ToPainkillerStatusDto(residentId, records)).ToArray();

        Assert.All(results, r => Assert.Equal(residentId, r.ResidentId));
    }
}
