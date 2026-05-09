// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs.Audit;
using Core.Interfaces.Managers;

namespace Infrastructure.Managers;

/// <summary>
/// Provides operations for retrieving audit history by communicating with the backend API over HTTP.
/// </summary>
/// <remarks>
/// Implements <see cref="IAuditManager"/> using <see cref="HttpClient"/> to call the WebApi.
/// Keeps all HTTP communication out of Core — Core only knows the interface contract.
/// Used by UC-009 (View History and Traceability).
/// </remarks>
/// <example>
/// <code>
/// // Registered in Infrastructure DependencyInjection:
/// services.AddScoped&lt;IAuditManager, AuditManager&gt;();
/// </code>
/// </example>
/// <remarks>
/// Initializes a new instance of the <see cref="AuditManager"/> class.
/// </remarks>
/// <param name="httpClientFactory">The factory used to create the named <see cref="HttpClient"/> for the API.</param>
/// <exception cref="InvalidOperationException">The named HttpClient 'SlottetApi' could not be created.</exception>
public class AuditManager(IHttpClientFactory httpClientFactory) : IAuditManager
{
    #region Fields

    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("SlottetApi")
            ?? throw new InvalidOperationException("Failed to create HttpClient.");

    #endregion

    #region Methods

    /// <summary>
    /// Gets recent audit entries ordered chronologically (most recent first).
    /// </summary>
    /// <param name="limit">The maximum number of entries to return. Use <see langword="null"/> for no limit.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of <see cref="AuditEntryDto"/> without change details.</returns>
    public async Task<IEnumerable<AuditEntryDto>> GetRecentAsync(int? limit, CancellationToken cancellationToken)
    {
        string url = limit.HasValue
            ? $"audit/recent?limit={limit.Value}"
            : "audit/recent";

        return await _httpClient.GetFromJsonAsync<IEnumerable<AuditEntryDto>>(url, cancellationToken) ?? [];
    }

    /// <summary>
    /// Gets audit entries filtered by the entity type that was changed.
    /// </summary>
    /// <param name="entityName">The entity type name to filter by (e.g. "Resident").</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of <see cref="AuditEntryDto"/> for the specified entity type.</returns>
    public async Task<IEnumerable<AuditEntryDto>> GetByEntityNameAsync(string entityName, CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<AuditEntryDto>>(
            $"audit/entity/{Uri.EscapeDataString(entityName)}", cancellationToken) ?? [];
    }

    /// <summary>
    /// Gets a single audit entry including its associated change details.
    /// </summary>
    /// <param name="id">The identifier of the audit entry.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The <see cref="AuditEntryDto"/> with change details, or <see langword="null"/> if not found.</returns>
    public async Task<AuditEntryDto?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"audit/{id}", cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        _ = response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AuditEntryDto>(cancellationToken: cancellationToken);
    }

    #endregion
}
