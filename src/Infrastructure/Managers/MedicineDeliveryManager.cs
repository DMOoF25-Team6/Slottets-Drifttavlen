// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.Net;
using System.Net.Http.Json;

using Core.DTOs;
using Core.Interfaces.Managers;

namespace Infrastructure.Managers;

/// <summary>
/// Communicates with the backend API over HTTP to manage medicine deliveries.
/// </summary>
/// <remarks>
/// Implements <see cref="IMedicineDeliveryManager"/> against the "SlottetApi" HttpClient,
/// mirroring the pattern used by <see cref="ResidentManager"/>.
/// </remarks>
public class MedicineDeliveryManager(IHttpClientFactory httpClientFactory)
    : HttpApiManagerBase(httpClientFactory, "SlottetApi"), IMedicineDeliveryManager
{
    public async Task<IEnumerable<MedicineDeliveryResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        IEnumerable<MedicineDeliveryResponseDto>? response =
            await HttpClient.GetFromJsonAsync<IEnumerable<MedicineDeliveryResponseDto>>("medicinedelivery", ct);
        return response ?? [];
    }

    public async Task<MedicineDeliveryResponseDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            return await HttpClient.GetFromJsonAsync<MedicineDeliveryResponseDto>($"medicinedelivery/{id}", ct);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task CreateAsync(MedicineDeliveryCreateRequestDto dto, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("medicinedelivery", dto, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to create medicine delivery. Status code: {response.StatusCode}");
        }
    }

    public async Task UpdateAsync(Guid id, MedicineDeliveryUpdateRequestDto dto, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        HttpResponseMessage response = await HttpClient.PutAsJsonAsync($"medicinedelivery/{id}", dto, ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to update medicine delivery. Status code: {response.StatusCode}");
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        HttpResponseMessage response = await HttpClient.DeleteAsync($"medicinedelivery/{id}", ct);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to delete medicine delivery. Status code: {response.StatusCode}");
        }
    }
}
