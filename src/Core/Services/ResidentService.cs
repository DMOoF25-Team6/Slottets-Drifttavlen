// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;
using Core.Mappers;

using Domain.Entities;
using Domain.Enums;

namespace Core.Services;

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
public class ResidentService(IResidentManager residentManager) : IResidentService
{
    /// <summary>
    /// Retrieves a resident by their unique identifier. Validates the input ID and throws an exception if it is invalid.
    /// </summary>
    /// <param name="id">The unique identifier of the resident.</param>
    /// <param name="ct">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the resident if found; otherwise, <see langword="null"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided ID is invalid.</exception>
    public async Task<Resident?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        // Validation logic
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Beboer ID må ikke være tomt.", nameof(id));
        }

        ResidentResponseDto? resident = await residentManager.GetByIdAsync(id, ct);
        return resident is null ? null : ResidentMapper.ToResident(resident);
    }

    /// <summary>
    /// Asynchronously retrieves all residents.
    /// </summary>
    /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all residents.</returns>
    public async Task<IEnumerable<Resident>> GetAllAsync(CancellationToken ct = default)
    {
        IEnumerable<ResidentResponseDto> residents = await residentManager.GetAllAsync(ct);
        return residents.Select(ResidentMapper.ToResident);
    }

    /// <summary>
    /// Asynchronously retrieves residents belonging to the specified departments. This method allows filtering residents based on their department affiliation.
    /// </summary>
    /// <param name="departments">A list of departments for which to retrieve the associated residents. Cannot be null.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of residents corresponding to the specified departments.</returns>
    public async Task<IEnumerable<Resident>> GetByDepartmentsAsync(IList<Department> departments, CancellationToken ct = default)
    {
        IEnumerable<ResidentResponseDto> residents = await residentManager.GetByDepartmentsAsync(departments, ct);
        return residents.Select(ResidentMapper.ToResident);
    }

    /// <summary>
    /// Creates a new resident using the specified data transfer object asynchronously.
    /// </summary>
    /// <param name="dto">The data transfer object containing the information required to create a new resident. Cannot be null.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous create operation.</returns>
    public async Task CreateAsync(ResidentCreateRequestDto dto, CancellationToken ct = default)
    {
        // Validation logic
        ArgumentNullException.ThrowIfNull(dto);
        _ = ResidentMapper.ToResident(dto);
        _ = residentManager.CreateAsync(dto, ct);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Asynchronously updates the information of an existing resident using the specified update request.
    /// </summary>
    /// <param name="id">The unique identifier of the resident to update.</param>
    /// <param name="resident">The data transfer object containing the updated resident information to apply.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the update operation.</param>
    /// <returns>A task that represents the asynchronous update operation.</returns>
    public async Task UpdateAsync(Guid id, ResidentUpdateRequestDto resident, CancellationToken ct = default)
    {
        await residentManager.UpdateAsync(id, resident, ct);
    }

    /// <summary>
    /// Asynchronously deletes the resident with the specified unique identifier, if it exists.
    /// </summary>
    /// <remarks>If no resident with the specified identifier exists, the method completes without performing
    /// any action.</remarks>
    /// <param name="id">The unique identifier of the resident to delete.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the delete operation.</param>
    /// <exception cref="ArgumentException">Thrown when the provided ID is invalid.</exception>
    /// <returns></returns>
    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Beboer ID må ikke være tomt.", nameof(id));
        }
        Resident? resident = await GetByIdAsync(id, ct);
        if (resident is not null)
        {
            await residentManager.DeleteAsync(resident.Id, ct);
        }
    }
}
