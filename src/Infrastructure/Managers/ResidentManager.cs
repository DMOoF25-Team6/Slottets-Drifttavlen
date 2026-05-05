// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs;
using Core.Interfaces.Managers;
using Core.Mappers;

using Domain.Entities;

namespace Infrastructure.Managers;

/// <summary>
/// Provides operations for managing residents by communicating with the backend API over HTTP.
/// </summary>
/// <remarks>
/// Implements <see cref="IResidentManager"/> for retrieving and manipulating resident data.
/// </remarks>
public class ResidentManager : IResidentManager
{
    #region Fields
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory? _httpClientFactory;
    #endregion

    public ResidentManager(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient("SlottetApi") ?? throw new InvalidOperationException("Failed to create HttpClient.");
    }

    #region Methods create
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
    public async Task<Resident> CreateAsync(Resident entity, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("residents/Create", entity, cancellationToken);
        _ = response.EnsureSuccessStatusCode();
        ResidentResponseDto? dto = await response.Content.ReadFromJsonAsync<ResidentResponseDto>(cancellationToken: cancellationToken);
        return dto != null ? ResidentMapper.ToResident(dto) : throw new InvalidOperationException("Failed to create resident.");
    }

    public Task<IEnumerable<Resident>> CreateRangeAsync(IEnumerable<Resident> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    #endregion create

    #region Methods read


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
    #endregion

    #region Methods update
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


    #endregion
}
