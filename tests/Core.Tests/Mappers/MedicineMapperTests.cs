// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Mappers;
using Domain.Entities;
using Xunit;

namespace Core.Tests.Mappers;

/// <summary>
/// Unit tests for <see cref="MedicineMapper"/> (UC-003).
/// </summary>
public class MedicineMapperTests
{
    [Fact]
    public void ToMedicineStatusDto_SetsResidentId()
    {
        Guid residentId = Guid.NewGuid();

        MedicineStatusDto dto = MedicineMapper.ToMedicineStatusDto(residentId, []);

        Assert.Equal(residentId, dto.ResidentId);
    }

    [Fact]
    public void ToMedicineStatusDto_EmptyRecords_ReturnsEmptyEntries()
    {
        MedicineStatusDto dto = MedicineMapper.ToMedicineStatusDto(Guid.NewGuid(), []);

        Assert.Empty(dto.Entries);
    }

    [Fact]
    public void ToMedicineStatusDto_MapsEntriesInOrder()
    {
        Guid residentId = Guid.NewGuid();
        List<MedicineRecord> records =
        [
            new() { Id = Guid.NewGuid(), ResidentId = residentId, MedicineName = "Panodil", Timestamp = DateTime.UtcNow, Given = true },
            new() { Id = Guid.NewGuid(), ResidentId = residentId, MedicineName = "Ipren", Timestamp = DateTime.UtcNow, Given = false }
        ];

        MedicineStatusDto dto = MedicineMapper.ToMedicineStatusDto(residentId, records);

        Assert.Equal(2, dto.Entries.Count);
        Assert.Equal("Panodil", dto.Entries[0].Name);
        Assert.True(dto.Entries[0].Given);
        Assert.Equal("Ipren", dto.Entries[1].Name);
        Assert.False(dto.Entries[1].Given);
    }

    [Fact]
    public void ToMedicineStatusDto_NullRecords_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            MedicineMapper.ToMedicineStatusDto(Guid.NewGuid(), null!));
    }
}
