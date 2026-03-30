// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


using Core.Interfaces.ApiClients;
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
    private readonly IResidentApiClient _residentApiClient;


    /// <summary>
    /// Initializes a new instance of the <see cref="ResidentService"/> class.
    /// </summary>
    /// <param name="residentRepository">The repository abstraction for resident data access.</param>
    ///  The dependency injection system now provides: a ResidentRepository and an HttpClient to the ResidentService.
    /// The service can now make HTTP requests to an API.
    /// 
    public ResidentService(IResidentRepository residentRepository, IResidentApiClient residentApiClient)
    {
        _residentRepository = residentRepository;
        _residentApiClient = residentApiClient;

    }



    /// <summary>
    /// Retrieves a resident by their unique identifier by calling the external API.
    /// </summary>
    /// <param name="id">The unique identifier of the resident.</param>
    /// <param name="cancellationToken">Optional cancellation token for the operation.</param>
    /// <returns>
    /// A <see cref="Task"/> containing the <see cref="Resident"/> if found; otherwise, <c>null</c>.
    /// </returns>
    /// <remarks>
    /// This method is now refactored to use an API instead of directly accessing the repository.
    /// </remarks>
    public Task<Resident?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _residentApiClient.GetByIdAsync(id, cancellationToken);

    }

    /// <summary>
    /// Retrieves all residents  by calling the API.
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token for the operation.</param>
    /// <returns>
    /// A <see cref="Task"/> containing an <see cref="IEnumerable{Resident}"/> of all residents.If no residents are returned from the API,
    /// an empty collection is returned instead of null.
    /// </returns>
    /// <remarks>
    /// This method is refactored to use an API instead of the repository.
    /// The <see cref="HttpClient"/> sends a GET request to the endpoint "api/residents".
    /// The JSON response from the API is automatically deserialized into a collection of
    /// <see cref="Resident"/> objects.
    /// </remarks>
    public Task<IEnumerable<Resident>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _residentApiClient.GetAllAsync(cancellationToken);
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
