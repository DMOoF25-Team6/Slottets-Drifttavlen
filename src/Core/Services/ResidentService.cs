// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

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

public class ResidentService
{
    private readonly IResidentRepository _residentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResidentService"/> class.
    /// </summary>
    /// <param name="residentRepository">The repository abstraction for resident data access.</param>
    public ResidentService(IResidentRepository residentRepository)
    {
        _residentRepository = residentRepository;
    }

    /// <summary>
    /// Retrieves a resident by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the resident.</param>
    /// <param name="cancellationToken">Optional cancellation token for the operation.</param>
    /// <returns>
    /// A <see cref="Task"/> containing the <see cref="Resident"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public Task<Resident?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _residentRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <summary>
    /// Retrieves all residents.
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token for the operation.</param>
    /// <returns>
    /// A <see cref="Task"/> containing an <see cref="IEnumerable{Resident}"/> of all residents.
    /// </returns>
    public Task<IEnumerable<Resident>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _residentRepository.GetAllAsync(cancellationToken);
    }

    /// <summary>
    /// Adds a new resident to the data store.
    /// </summary>
    /// <param name="resident">The resident entity to add.</param>
    /// <param name="cancellationToken">Optional cancellation token for the operation.</param>
    /// <returns>
    /// A <see cref="Task"/> containing the added <see cref="Resident"/> entity.
    /// </returns>
    public Task<Resident> AddAsync(Resident resident, CancellationToken cancellationToken = default)
    {
        return _residentRepository.AddAsync(resident, cancellationToken);
    }

    /// <summary>
    /// Updates an existing resident in the data store.
    /// </summary>
    /// <param name="resident">The resident entity with updated values.</param>
    /// <param name="cancellationToken">Optional cancellation token for the operation.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task UpdateAsync(Resident resident, CancellationToken cancellationToken = default)
    {
        return _residentRepository.UpdateAsync(resident, cancellationToken);
    }

    /// <summary>
    /// Deletes a resident from the data store.
    /// </summary>
    /// <param name="resident">The resident entity to delete.</param>
    /// <param name="cancellationToken">Optional cancellation token for the operation.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task DeleteAsync(Resident resident, CancellationToken cancellationToken = default)
    {
        return _residentRepository.DeleteAsync(resident, cancellationToken);
    }
}
