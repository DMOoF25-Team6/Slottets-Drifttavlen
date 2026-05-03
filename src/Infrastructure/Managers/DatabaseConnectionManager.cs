// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.Interfaces.Managers;
using Core.Providers;

namespace Infrastructure.Managers;

/// <summary>
/// Manages database connection state by calling the API and updating the state provider.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DatabaseConnectionManager"/> class.
/// </remarks>
/// <param name="httpClient">The HTTP client for API calls.</param>
/// <param name="stateProvider">The state provider to update.</param>
public class DatabaseConnectionManager(HttpClient httpClient, DatabaseConnectionStateProvider stateProvider) : IDatabaseConnectionManager
{
    private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private readonly DatabaseConnectionStateProvider _stateProvider = stateProvider ?? throw new ArgumentNullException(nameof(stateProvider));

    /// <summary>
    /// Calls the API to check if the database is connected and updates the state provider.
    /// </summary>
    public async Task CheckAndUpdateConnectionStateAsync()
    {
        try
        {
            // Adjust the endpoint as needed
            bool isConnected = await _httpClient.GetFromJsonAsync<bool>("database/isconnected");
            _stateProvider.SetConnectionState(isConnected);
        }
        catch
        {
            _stateProvider.SetConnectionState(false);
        }
    }
}
