// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs.Security;
using Core.Interfaces.Managers;

namespace Infrastructure.Managers;

/// <summary>
/// Provides operations for managing security incidents by communicating with the backend API over HTTP (UC-010).
/// </summary>
/// <remarks>
/// Escalation with isBreach=true triggers GDPR Art. 33 notification on the server side.
/// </remarks>
public class SecurityIncidentManager : ISecurityIncidentManager
{
    #region Fields

    private readonly HttpClient _httpClient;

    #endregion

    #region Constructors

    public SecurityIncidentManager(IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        _httpClient = httpClientFactory.CreateClient("SlottetApi")
            ?? throw new InvalidOperationException("Failed to create HttpClient.");
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<SecurityIncidentDto>> GetIncidentsAsync(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<SecurityIncidentDto>>(
            "securityincident", cancellationToken) ?? [];
    }

    public async Task<SecurityIncidentDto> EscalateIncidentAsync(Guid incidentId, bool isBreach, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
            $"securityincident/{incidentId}/escalate", new { IsBreach = isBreach }, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SecurityIncidentDto>(cancellationToken)
            ?? throw new InvalidOperationException("Failed to deserialize escalated incident.");
    }

    public async Task<SecurityIncidentDto> AddInvestigationNotesAsync(AddInvestigationNotesDto dto, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
            $"securityincident/{dto.IncidentId}/notes", dto, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SecurityIncidentDto>(cancellationToken)
            ?? throw new InvalidOperationException("Failed to deserialize incident with notes.");
    }

    public async Task<SecurityIncidentDto> CloseIncidentAsync(Guid incidentId, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
            $"securityincident/{incidentId}/close", new { }, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SecurityIncidentDto>(cancellationToken)
            ?? throw new InvalidOperationException("Failed to deserialize closed incident.");
    }

    #endregion
}
