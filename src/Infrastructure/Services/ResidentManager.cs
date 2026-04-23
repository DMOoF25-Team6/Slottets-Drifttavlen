// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs;
using Core.Interfaces.Managers;
using Core.Mappers;

using Domain.Entities;

namespace Infrastructure.Services;

/// <summary>
/// Provides operations for managing residents by communicating with the backend API over HTTP.
/// </summary>
/// <remarks>
/// Implements <see cref="IResidentManager"/> for retrieving and manipulating resident data.
/// </remarks>
public class ResidentManager(HttpClient httpClient) : IResidentManager
{
    private readonly HttpClient _httpClient = httpClient;

    /// <summary>
    /// Gets a resident by their unique identifier.
    /// </summary>
    /// <param name="id">A unique identifier for the resident.</param>
    /// <param name="ct">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a resident if found; otherwise, <see langword="null"/>.
    /// </returns>
    public async Task<Resident?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        ResidentResponseDto? dto =
            await _httpClient.GetFromJsonAsync<ResidentResponseDto>(
                $"residents/{id}", ct);
        return dto != null ? ResidentMapper.ToResident(dto) : null;
    }

    /// <summary>
    /// Gets all residents.
    /// </summary>
    /// <param name="ct">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of residents.
    /// </returns>
    public async Task<IEnumerable<Resident>> GetAllAsync(CancellationToken ct = default)
    {
        IEnumerable<ResidentResponseDto>? dtos =
            await _httpClient.GetFromJsonAsync<IEnumerable<ResidentResponseDto>>(
                "residents", ct);
        return dtos != null ? dtos.Select(ResidentMapper.ToResident) : [];
    }

    /// <summary>
    /// Adds a new resident.
    /// </summary>
    /// <param name="entity">A resident entity to add.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the added resident.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Always thrown as this method is not implemented.
    /// </exception>
    public Task<Resident> AddAsync(Resident entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Adds a range of residents.
    /// </summary>
    /// <param name="entities">A collection of resident entities to add.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the added residents.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Always thrown as this method is not implemented.
    /// </exception>
    public Task<IEnumerable<Resident>> AddRangeAsync(IEnumerable<Resident> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates a resident.
    /// </summary>
    /// <param name="entity">A resident entity to update.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Always thrown as this method is not implemented.
    /// </exception>
    public Task UpdateAsync(Resident entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates a range of residents.
    /// </summary>
    /// <param name="entities">A collection of resident entities to update.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Always thrown as this method is not implemented.
    /// </exception>
    public Task UpdateRangeAsync(IEnumerable<Resident> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deletes a resident.
    /// </summary>
    /// <param name="entity">A resident entity to delete.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Always thrown as this method is not implemented.
    /// </exception>
    public Task DeleteAsync(Resident entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deletes a range of residents.
    /// </summary>
    /// <param name="entities">A collection of resident entities to delete.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Always thrown as this method is not implemented.
    /// </exception>
    public Task DeleteRangeAsync(IEnumerable<Resident> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
