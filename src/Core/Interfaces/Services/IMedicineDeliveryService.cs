// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Domain.Entities;

namespace Core.Interfaces.Services;

/// <summary>
/// Provides business operations for medicine deliveries.
/// </summary>
/// <remarks>
/// Depends on <see cref="Core.Interfaces.Managers.IMedicineDeliveryManager"/> for data access,
/// mirroring the Service -> Manager pattern used by resident management.
/// </remarks>
public interface IMedicineDeliveryService
{
    Task<IEnumerable<MedicineRecord>> GetAllAsync(CancellationToken ct = default);
    Task<MedicineRecord?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task CreateAsync(MedicineDeliveryCreateRequestDto dto, CancellationToken ct = default);
    Task UpdateAsync(Guid id, MedicineDeliveryUpdateRequestDto dto, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
