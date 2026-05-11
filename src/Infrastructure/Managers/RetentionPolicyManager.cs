// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs.Retention;
using Core.Interfaces.Managers;

namespace Infrastructure.Managers;

/// <summary>
/// Provides operations for managing retention policies by communicating with the backend API over HTTP (UC-010).
/// </summary>
/// <remarks>
/// Implements <see cref="IRetentionPolicyManager"/> using <see cref="HttpClient"/> to call the WebApi.
/// Keeps all HTTP communication out of Core — Core only knows the interface contract.
/// </remarks>
public class RetentionPolicyManager : IRetentionPolicyManager
{
    #region Fields

    private readonly HttpClient _httpClient;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="RetentionPolicyManager"/> class.
    /// </summary>
    /// <param name="httpClientFactory">The factory used to create the named <see cref="HttpClient"/> for the API.</param>
    /// <exception cref="InvalidOperationException">The named HttpClient 'SlottetApi' could not be created.</exception>
    public RetentionPolicyManager(IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        _httpClient = httpClientFactory.CreateClient("SlottetApi")
            ?? throw new InvalidOperationException("Failed to create HttpClient.");
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<RetentionPolicyDto>> GetPoliciesAsync(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<RetentionPolicyDto>>(
            "retentionpolicy", cancellationToken) ?? [];
    }

    public async Task<RetentionPolicyDto> UpdateRetentionPolicyAsync(
        UpdateRetentionPolicyDto dto,
        Guid changedByEmployeeId,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync(
            $"retentionpolicy?changedByEmployeeId={changedByEmployeeId}", dto, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RetentionPolicyDto>(cancellationToken)
            ?? throw new InvalidOperationException("Failed to deserialize updated retention policy.");
    }

    #endregion
}
