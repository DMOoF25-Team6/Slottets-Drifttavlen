// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;
using Core.DTOs;

namespace WebUI.Client.Services;

public class MedicineDeliveryClient
{
    private readonly HttpClient _http;

    public MedicineDeliveryClient(IHttpClientFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _http = factory.CreateClient("SlottetApi");
    }

    public async Task<IEnumerable<MedicineDeliveryResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        IEnumerable<MedicineDeliveryResponseDto>? data =
            await _http.GetFromJsonAsync<IEnumerable<MedicineDeliveryResponseDto>>("medicinedelivery", ct);
        return data ?? Array.Empty<MedicineDeliveryResponseDto>();
    }

    public async Task<MedicineDeliveryResponseDto?> CreateAsync(MedicineDeliveryCreateRequestDto dto, CancellationToken ct = default)
    {
        HttpResponseMessage response = await _http.PostAsJsonAsync("medicinedelivery", dto, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<MedicineDeliveryResponseDto>(cancellationToken: ct);
    }

    public async Task<bool> UpdateAsync(Guid id, MedicineDeliveryUpdateRequestDto dto, CancellationToken ct = default)
    {
        HttpResponseMessage response = await _http.PutAsJsonAsync($"medicinedelivery/{id}", dto, ct);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        HttpResponseMessage response = await _http.DeleteAsync($"medicinedelivery/{id}", ct);
        return response.IsSuccessStatusCode;
    }
}
