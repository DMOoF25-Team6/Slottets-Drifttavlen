// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.Interfaces.Managers;

using Domain.Entities;


namespace Infrastructure.Services;

public class ResidentManager(HttpClient httpClient) : IResidentManager
{
    private readonly HttpClient _httpClient = httpClient;

    public Task<Resident?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return _httpClient.GetFromJsonAsync<Resident>(
            $"api/residents/{id}", ct);
    }

    public async Task<IEnumerable<Resident>> GetAllAsync(CancellationToken ct = default) => await _httpClient.GetFromJsonAsync<IEnumerable<Resident>>(
            "api/residents", ct) ?? [];

    public Task<Resident> AddAsync(Resident entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Resident>> AddRangeAsync(IEnumerable<Resident> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Resident entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(IEnumerable<Resident> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Resident entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRangeAsync(IEnumerable<Resident> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}