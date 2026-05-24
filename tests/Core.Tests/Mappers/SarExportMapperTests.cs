// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs.Sar;
using Core.Mappers;

namespace Core.Tests.Mappers;

public class SarExportMapperTests
{
    [Fact]
    [Trait("Category", "Functionality")]
    public void ToPackageDto_ThreeArgs_MapsMetadata()
    {
        Guid id = Guid.NewGuid();
        DateTime now = DateTime.UtcNow;

        SarExportPackageDto dto = SarExportMapper.ToPackageDto(id, now, "f.json");

        Assert.Equal(id, dto.ExportId);
        Assert.Equal(now, dto.GeneratedAt);
        Assert.Equal("f.json", dto.FileName);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public void ToPackageDto_WithPayload_MapsPayload()
    {
        Guid id = Guid.NewGuid();

        SarExportPackageDto dto = SarExportMapper.ToPackageDto(id, DateTime.UtcNow, "f.json", "{\"k\":1}");

        Assert.Equal("{\"k\":1}", dto.Payload);
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public void ToPackageDto_EmptyFileName_Maps()
    {
        SarExportPackageDto dto = SarExportMapper.ToPackageDto(Guid.NewGuid(), DateTime.UtcNow, string.Empty);

        Assert.Equal(string.Empty, dto.FileName);
    }

    [Fact]
    [Trait("Category", "Concurrency")]
    public void ToPackageDto_ParallelMapping_IsThreadSafe()
    {
        Guid id = Guid.NewGuid();

        SarExportPackageDto[] results = Enumerable.Range(0, 200).AsParallel()
            .Select(_ => SarExportMapper.ToPackageDto(id, DateTime.UtcNow, "f.json")).ToArray();

        Assert.All(results, r => Assert.Equal(id, r.ExportId));
    }
}
