// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

namespace Core.Interfaces.Services;

public interface IMedicineDeliveryService
{
    Task<MedicineDeliveryResponseDto> CreateAsync(MedicineDeliveryCreateRequestDto dto, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, MedicineDeliveryUpdateRequestDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<MedicineDeliveryResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MedicineDeliveryResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
