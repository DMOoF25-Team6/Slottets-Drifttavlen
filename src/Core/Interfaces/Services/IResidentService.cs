// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

using Domain.Entities;

namespace Core.Interfaces.Services;

/// <summary>
/// Provides operations for managing residents, including creation, retrieval, update, and deletion.
/// </summary>
/// <remarks>
/// This service defines the contract for resident management in the application. Implementations should handle all business logic and data access for resident entities.
/// </remarks>
public interface IResidentService
{
    /// <summary>
    /// Creates a new resident using the specified data transfer object.
    /// </summary>
    /// <param name="dto">An object containing the data required to create a resident.</param>
    /// <param name="ct">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A task that represents the asynchronous create operation.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="dto"/> parameter is <see langword="null"/>.</exception>
    Task CreateAsync(ResidentCreateRequestDto dto, CancellationToken ct = default);

    /// <summary>
    /// Deletes the resident with the specified identifier.
    /// </summary>
    /// <param name="id">A unique identifier of the resident to delete.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    /// <exception cref="KeyNotFoundException">No resident with the specified <paramref name="id"/> exists.</exception>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all residents.
    /// </summary>
    /// <param name="ct">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of residents.</returns>
    Task<IEnumerable<Resident>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Retrieves a resident by its unique identifier.
    /// </summary>
    /// <param name="id">A unique identifier of the resident to retrieve.</param>
    /// <param name="ct">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the resident if found; otherwise, <see langword="null"/>.</returns>
    Task<Resident?> GetByIdAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing resident with the specified data.
    /// </summary>
    /// <param name="resident">An object containing the updated resident data.</param>
    /// <param name="ct">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A task that represents the asynchronous update operation.</returns>
    /// <exception cref="KeyNotFoundException">No resident with the specified identifier exists.</exception>
    Task UpdateAsync(ResidentUpdateRequestDto resident, CancellationToken ct = default);
}