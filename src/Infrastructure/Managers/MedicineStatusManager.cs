// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs;
using Core.Interfaces.Managers;

namespace Infrastructure.Managers;
/// <summary>
/// Provides HTTP-based access to medicine and painkiller status from the API.
/// </summary>


public class MedicineStatusManager(IHttpClientFactory httpClientFactory) : IMedicineStatusManager
{

    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("SlottetApi")
            ?? throw new InvalidOperationException("Failed to create HttpClient.");



    /// <summary>
    /// Retrieves medicine status for a resident.
    /// </summary>
    public async Task<MedicineStatusDto?> GetMedicineStatusAsync(Guid residentId, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<MedicineStatusDto>(
            $"Medicine/{residentId}",
            cancellationToken);
    }

    /// <summary>
    /// Retrieves painkiller status for a resident.
    /// </summary>
    public async Task<PainkillerStatusDto?> GetPainkillerStatusAsync(Guid residentId, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<PainkillerStatusDto>(
            $"Medicine/painkiller/{residentId}",
            cancellationToken);
    }
}
