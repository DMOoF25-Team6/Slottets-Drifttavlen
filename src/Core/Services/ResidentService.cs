// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.



using Core.DTOs;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;
using Core.Mappers;

using Domain.Entities;

/// <summary>
/// Service responsible for managing <see cref="Resident"/> entities, providing business logic and orchestration
/// between the application and data access layers. This class encapsulates CRUD operations for residents,
/// ensuring that all interactions with the data store are performed through the <see cref="IResidentRepository"/> abstraction.
/// </summary>
/// <remarks>
/// Implements Clean Architecture principles by depending only on abstractions. All methods are asynchronous to support
/// scalable, non-blocking operations. This service should be used by higher-level application components (e.g., Blazor pages, API controllers)
/// to perform resident-related operations.
/// </remarks>
namespace Core.Services;

public class ResidentService : IResidentService
{
    private readonly IResidentManager _residentManager;

    public ResidentService(IResidentManager residentManager)
    {
        _residentManager = residentManager;
    }

    public async Task<ResidentResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Resident? resident = await _residentManager.GetByIdAsync(id, cancellationToken);
        return resident is null ? null : ResidentMapper.ToResidentResponseDto(resident);
    }

    public async Task<IEnumerable<ResidentResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<Resident> residents = await _residentManager.GetAllAsync(cancellationToken);
        return residents.Select(ResidentMapper.ToResidentResponseDto);
    }

    public async Task<ResidentResponseDto> CreateAsync(ResidentRequest dto, CancellationToken cancellationToken = default)
    {
        var resident = ResidentMapper.ToResident(dto);
        Resident created = await _residentManager.CreateAsync(resident, cancellationToken);
        return ResidentMapper.ToResidentResponseDto(created);
    }

    public async Task UpdateAsync(ResidentRequest dto, CancellationToken cancellationToken = default)
    {
        var resident = ResidentMapper.ToResident(dto);
        await _residentManager.UpdateAsync(resident, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Resident? resident = await _residentManager.GetByIdAsync(id, cancellationToken);
        if (resident is not null)
        {
            await _residentManager.DeleteAsync(resident, cancellationToken);
        }
    }
}
