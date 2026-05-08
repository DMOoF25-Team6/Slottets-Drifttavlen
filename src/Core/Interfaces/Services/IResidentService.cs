// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

namespace Core.Interfaces.Services;

/// <summary>
/// Defines a contract for managing residents in the system.
/// </summary>
/// <remarks>
/// Inherits CRUD operations from <see cref="ICRUD{Resident}"/>.
/// </remarks>
public interface IResidentService
{
    Task<ResidentResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ResidentResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ResidentResponseDto> CreateAsync(ResidentRequest dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(ResidentRequest dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
