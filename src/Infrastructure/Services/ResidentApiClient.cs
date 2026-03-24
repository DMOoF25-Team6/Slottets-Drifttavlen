// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.Interfaces.ApiClients;

using Domain.Entities;


namespace Infrastructure.Services;

public class ResidentApiClient : IResidentApiClient
{
    private readonly HttpClient _httpClient;

    public ResidentApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<Resident?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return _httpClient.GetFromJsonAsync<Resident>(
            $"api/residents/{id}", ct);
    }

    public async Task<IEnumerable<Resident>> GetAllAsync(CancellationToken ct = default)
    {
        IEnumerable<Resident>? residents = await _httpClient.GetFromJsonAsync<IEnumerable<Resident>>(
            "api/residents", ct);

        return residents ?? [];
    }
}