// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs;
using Core.Interfaces.Managers;

namespace Infrastructure.Services;

/// <summary>
/// Provides operations for managing phone assignments by communicating with the backend API over HTTP.
/// </summary>
public class PhoneAssignmentManager(HttpClient httpClient) : IPhoneAssignmentManager
{
    private readonly HttpClient _httpClient = httpClient;

    /// <summary>
    /// Gets the current phone assignments for the active shift.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A collection of phone assignments for the active shift.</returns>
    public async Task<IEnumerable<PhoneAssignmentDto>> GetCurrentPhoneAssignmentsForActiveShift(CancellationToken cancellationToken)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<PhoneAssignmentDto>>(
            "api/phoneassignments/active-shift", cancellationToken) ?? [];
    }
}
