// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Domain.Entities;

namespace Core.Mappers;

public static class MedicineDeliveryMapper
{
    public static MedicineRecord ToMedicineRecord(MedicineDeliveryCreateRequestDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        return new MedicineRecord
        {
            Id = Guid.NewGuid(),
            ResidentId = dto.ResidentId,
            MedicineName = dto.MedicineName,
            Timestamp = dto.Timestamp,
            Given = dto.Given
        };
    }

    public static void ApplyUpdate(MedicineRecord record, MedicineDeliveryUpdateRequestDto dto)
    {
        ArgumentNullException.ThrowIfNull(record);
        ArgumentNullException.ThrowIfNull(dto);
        record.ResidentId = dto.ResidentId;
        record.MedicineName = dto.MedicineName;
        record.Timestamp = dto.Timestamp;
        record.Given = dto.Given;
    }

    public static MedicineDeliveryResponseDto ToResponseDto(MedicineRecord record)
    {
        ArgumentNullException.ThrowIfNull(record);
        return new MedicineDeliveryResponseDto
        {
            Id = record.Id,
            ResidentId = record.ResidentId,
            MedicineName = record.MedicineName,
            Timestamp = record.Timestamp,
            Given = record.Given
        };
    }
}
