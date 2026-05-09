// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs;
using Core.Interfaces.Managers;

using Domain.Entities;

namespace Infrastructure.Managers;

/// <summary>
/// Provides operations for managing residents by communicating with the backend API over HTTP.
/// </summary>
/// <remarks>
/// Implements <see cref="IResidentManager"/> for retrieving and manipulating resident data.
/// </remarks>
public class ResidentManager(IHttpClientFactory httpClientFactory) : HttpApiManagerBase(httpClientFactory, "SlottetApi"), IResidentManager
{
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
    public async Task CreateAsync(ResidentCreateRequestDto dto, CancellationToken ct = default)
    {
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("residents/Create", dto, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to create resident. Status code: " + response.StatusCode);
        }
    }


    public async Task CreateRangeAsync(IEnumerable<ResidentCreateRequestDto> dtos, CancellationToken ct = default)
    {
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("residents/CreateRange", dtos, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to create residents. Status code: " + response.StatusCode);
        }
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
    public async Task<ResidentResponseDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            ResidentResponseDto? response = await HttpClient.GetFromJsonAsync<ResidentResponseDto>($"residents/{id}", ct);
            return response;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    /// <summary>
    /// Gets all residents.
    /// </summary>
    /// <param name="ct">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of residents.
    /// </returns>
    public async Task<IEnumerable<ResidentResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        IEnumerable<ResidentResponseDto>? response = await HttpClient.GetFromJsonAsync<IEnumerable<ResidentResponseDto>>("residents", ct);
        return response ?? [];
    }
    #endregion

    #region Methods update
    /// <summary>
    /// Updates a resident.
    /// </summary>
    /// <param name="id">A unique identifier for the resident to update.</param>
    /// <param name="entity">A resident entity to update.</param>
    /// <param name="ct">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public async Task UpdateAsync(Guid id, ResidentUpdateRequestDto entity, CancellationToken ct = default)
    {
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync($"residents/{id}", entity, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to update resident. Status code: " + response.StatusCode);
        }
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
    public Task UpdateRangeAsync(IEnumerable<ResidentUpdateRequestDto> entities, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deletes a resident.
    /// </summary>
    /// <param name="id">The unique identifier of the resident to delete.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Always thrown as this method is not implemented.
    /// </exception>
    public Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deletes a range of residents.
    /// </summary>
    /// <param name="ids">A collection of resident IDs to delete.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Always thrown as this method is not implemented.
    /// </exception>
    public Task DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Resident entity, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task CreateRangeAsync(IEnumerable<Resident> entities, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    #endregion
}
