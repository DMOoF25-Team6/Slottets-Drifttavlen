// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs.Anonymization;
using Core.Interfaces.Managers;

namespace Infrastructure.Managers;

/// <summary>
/// Provides operations for managing anonymization candidates by communicating with the backend API over HTTP (UC-010).
/// </summary>
public class AnonymizationManager : IAnonymizationManager
{
    #region Fields

    private readonly HttpClient _httpClient;

    #endregion

    #region Constructors

    public AnonymizationManager(IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        _httpClient = httpClientFactory.CreateClient("SlottetApi")
            ?? throw new InvalidOperationException("Failed to create HttpClient.");
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<AnonymizationCandidateDto>> GetCandidatesAsync(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<AnonymizationCandidateDto>>(
            "anonymization/candidates", cancellationToken) ?? [];
    }

    public async Task<AnonymizationResultDto> ApproveAnonymizationAsync(Guid candidateId, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
            $"anonymization/{candidateId}/approve", new { }, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AnonymizationResultDto>(cancellationToken)
            ?? throw new InvalidOperationException("Failed to deserialize anonymization result.");
    }

    public async Task<bool> RejectAnonymizationAsync(Guid candidateId, string reason, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
            $"anonymization/{candidateId}/reject", new { Reason = reason }, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    #endregion
}
