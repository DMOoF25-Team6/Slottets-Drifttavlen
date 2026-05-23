// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;
using Core.Mappers;

using Domain.Entities;

namespace Core.Services;

/// <summary>
/// Service responsible for medicine-delivery business logic. Delegates data access to
/// <see cref="IMedicineDeliveryManager"/> and maps response DTOs to <see cref="MedicineRecord"/>
/// domain entities, following Clean Architecture and the Dependency Inversion principle.
/// </summary>
public class MedicineDeliveryService(IMedicineDeliveryManager medicineDeliveryManager) : IMedicineDeliveryService
{
    public async Task<IEnumerable<MedicineRecord>> GetAllAsync(CancellationToken ct = default)
    {
        IEnumerable<MedicineDeliveryResponseDto> deliveries = await medicineDeliveryManager.GetAllAsync(ct);
        return deliveries.Select(MedicineDeliveryMapper.ToMedicineRecord);
    }

    public async Task<MedicineRecord?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        MedicineDeliveryResponseDto? dto = await medicineDeliveryManager.GetByIdAsync(id, ct);
        return dto is null ? null : MedicineDeliveryMapper.ToMedicineRecord(dto);
    }

    public async Task CreateAsync(MedicineDeliveryCreateRequestDto dto, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        await medicineDeliveryManager.CreateAsync(dto, ct);
    }

    public async Task UpdateAsync(Guid id, MedicineDeliveryUpdateRequestDto dto, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        await medicineDeliveryManager.UpdateAsync(id, dto, ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        await medicineDeliveryManager.DeleteAsync(id, ct);
    }
}
