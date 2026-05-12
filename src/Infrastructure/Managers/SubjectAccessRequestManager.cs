// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs.Sar;
using Core.Interfaces.Managers;

namespace Infrastructure.Managers;

/// <summary>
/// Provides operations for handling GDPR Subject Access Requests by communicating with the backend API over HTTP (UC-010, Art. 15).
/// </summary>
public class SubjectAccessRequestManager : ISubjectAccessRequestManager
{
    #region Fields

    private readonly HttpClient _httpClient;

    #endregion

    #region Constructors

    public SubjectAccessRequestManager(IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        _httpClient = httpClientFactory.CreateClient("SlottetApi")
            ?? throw new InvalidOperationException("Failed to create HttpClient.");
    }

    #endregion

    #region Methods

    public async Task<SarExportPackageDto> GenerateExportAsync(SarExportRequestDto dto, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
            "subjectaccessrequest/export", dto, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SarExportPackageDto>(cancellationToken)
            ?? throw new InvalidOperationException("Failed to deserialize SAR export package.");
    }

    public async Task<bool> MarkFulfilledAsync(SarFulfilledDto dto, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
            "subjectaccessrequest/fulfilled", dto, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    #endregion
}
