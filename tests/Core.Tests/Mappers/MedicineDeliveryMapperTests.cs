// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Mappers;
using Domain.Entities;
using Xunit;

namespace Core.Tests.Mappers;

/// <summary>
/// Unit tests for <see cref="MedicineDeliveryMapper"/> (UC-020/021/022).
/// </summary>
public class MedicineDeliveryMapperTests
{
    #region ToMedicineRecord (CreateRequestDto)

    [Fact]
    public void ToMedicineRecord_FromCreateDto_MapsAllFields()
    {
        Guid residentId = Guid.NewGuid();
        DateTime timestamp = new(2026, 5, 23, 8, 0, 0, DateTimeKind.Utc);
        MedicineDeliveryCreateRequestDto dto = new()
        {
            ResidentId = residentId,
            MedicineName = "Panodil",
            Timestamp = timestamp,
            Given = true
        };

        MedicineRecord result = MedicineDeliveryMapper.ToMedicineRecord(dto);

        Assert.Equal(residentId, result.ResidentId);
        Assert.Equal("Panodil", result.MedicineName);
        Assert.Equal(timestamp, result.Timestamp);
        Assert.True(result.Given);
    }

    [Fact]
    public void ToMedicineRecord_FromCreateDto_GeneratesNonEmptyId()
    {
        MedicineDeliveryCreateRequestDto dto = new()
        {
            ResidentId = Guid.NewGuid(),
            MedicineName = "Ipren",
            Timestamp = DateTime.UtcNow
        };

        MedicineRecord result = MedicineDeliveryMapper.ToMedicineRecord(dto);

        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public void ToMedicineRecord_FromCreateDto_TwoCalls_ProduceDifferentIds()
    {
        MedicineDeliveryCreateRequestDto dto = new()
        {
            ResidentId = Guid.NewGuid(),
            MedicineName = "Ipren",
            Timestamp = DateTime.UtcNow
        };

        MedicineRecord first = MedicineDeliveryMapper.ToMedicineRecord(dto);
        MedicineRecord second = MedicineDeliveryMapper.ToMedicineRecord(dto);

        Assert.NotEqual(first.Id, second.Id);
    }

    [Fact]
    public void ToMedicineRecord_FromNullCreateDto_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            MedicineDeliveryMapper.ToMedicineRecord((MedicineDeliveryCreateRequestDto)null!));
    }

    #endregion

    #region ToMedicineRecord (ResponseDto)

    [Fact]
    public void ToMedicineRecord_FromResponseDto_PreservesId()
    {
        Guid id = Guid.NewGuid();
        MedicineDeliveryResponseDto dto = new()
        {
            Id = id,
            ResidentId = Guid.NewGuid(),
            MedicineName = "Panodil",
            Timestamp = DateTime.UtcNow,
            Given = false
        };

        MedicineRecord result = MedicineDeliveryMapper.ToMedicineRecord(dto);

        Assert.Equal(id, result.Id);
        Assert.Equal(dto.ResidentId, result.ResidentId);
        Assert.Equal(dto.MedicineName, result.MedicineName);
        Assert.False(result.Given);
    }

    [Fact]
    public void ToMedicineRecord_FromNullResponseDto_Throws()
    {
        Assert.Throws<ArgumentNullException>(() =>
            MedicineDeliveryMapper.ToMedicineRecord((MedicineDeliveryResponseDto)null!));
    }

    #endregion

    #region ApplyUpdate

    [Fact]
    public void ApplyUpdate_OverwritesMutableFields_KeepsId()
    {
        Guid originalId = Guid.NewGuid();
        MedicineRecord record = new()
        {
            Id = originalId,
            ResidentId = Guid.NewGuid(),
            MedicineName = "Old",
            Timestamp = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            Given = false
        };
        Guid newResident = Guid.NewGuid();
        DateTime newTime = new(2026, 5, 23, 9, 0, 0, DateTimeKind.Utc);
        MedicineDeliveryUpdateRequestDto dto = new()
        {
            ResidentId = newResident,
            MedicineName = "New",
            Timestamp = newTime,
            Given = true
        };

        MedicineDeliveryMapper.ApplyUpdate(record, dto);

        Assert.Equal(originalId, record.Id);
        Assert.Equal(newResident, record.ResidentId);
        Assert.Equal("New", record.MedicineName);
        Assert.Equal(newTime, record.Timestamp);
        Assert.True(record.Given);
    }

    [Fact]
    public void ApplyUpdate_NullRecord_Throws()
    {
        MedicineDeliveryUpdateRequestDto dto = new() { MedicineName = "x", Timestamp = DateTime.UtcNow };
        Assert.Throws<ArgumentNullException>(() => MedicineDeliveryMapper.ApplyUpdate(null!, dto));
    }

    [Fact]
    public void ApplyUpdate_NullDto_Throws()
    {
        MedicineRecord record = new() { MedicineName = "x", Timestamp = DateTime.UtcNow };
        Assert.Throws<ArgumentNullException>(() => MedicineDeliveryMapper.ApplyUpdate(record, null!));
    }

    #endregion

    #region ToResponseDto

    [Fact]
    public void ToResponseDto_MapsAllFields()
    {
        MedicineRecord record = new()
        {
            Id = Guid.NewGuid(),
            ResidentId = Guid.NewGuid(),
            MedicineName = "Panodil",
            Timestamp = DateTime.UtcNow,
            Given = true
        };

        MedicineDeliveryResponseDto dto = MedicineDeliveryMapper.ToResponseDto(record);

        Assert.Equal(record.Id, dto.Id);
        Assert.Equal(record.ResidentId, dto.ResidentId);
        Assert.Equal(record.MedicineName, dto.MedicineName);
        Assert.Equal(record.Timestamp, dto.Timestamp);
        Assert.True(dto.Given);
    }

    [Fact]
    public void ToResponseDto_NullRecord_Throws()
    {
        Assert.Throws<ArgumentNullException>(() => MedicineDeliveryMapper.ToResponseDto(null!));
    }

    #endregion

    #region Concurrency

    [Fact]
    public void ToResponseDto_UnderParallelLoad_IsThreadSafe()
    {
        MedicineRecord[] records = Enumerable.Range(0, 500)
            .Select(i => new MedicineRecord
            {
                Id = Guid.NewGuid(),
                ResidentId = Guid.NewGuid(),
                MedicineName = $"med-{i}",
                Timestamp = DateTime.UtcNow,
                Given = i % 2 == 0
            })
            .ToArray();

        MedicineDeliveryResponseDto[] results = new MedicineDeliveryResponseDto[records.Length];
        Parallel.For(0, records.Length, i =>
        {
            results[i] = MedicineDeliveryMapper.ToResponseDto(records[i]);
        });

        for (int i = 0; i < records.Length; i++)
        {
            Assert.Equal(records[i].Id, results[i].Id);
            Assert.Equal(records[i].MedicineName, results[i].MedicineName);
        }
    }

    #endregion
}
