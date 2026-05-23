// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

namespace Core.Interfaces.Managers;

/// <summary>
/// Defines data-access operations for medicine deliveries.
/// </summary>
/// <remarks>
/// Implemented in the Infrastructure layer by an HTTP client that talks to the WebApi.
/// Core depends only on this abstraction (Dependency Inversion).
/// </remarks>
public interface IMedicineDeliveryManager
{
    Task<IEnumerable<MedicineDeliveryResponseDto>> GetAllAsync(CancellationToken ct = default);
    Task<MedicineDeliveryResponseDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task CreateAsync(MedicineDeliveryCreateRequestDto dto, CancellationToken ct = default);
    Task UpdateAsync(Guid id, MedicineDeliveryUpdateRequestDto dto, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
