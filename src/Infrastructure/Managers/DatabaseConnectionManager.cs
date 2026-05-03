// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.Interfaces.Managers;
using Core.Interfaces.Providers;

namespace Infrastructure.Managers;

/// <summary>
/// Manages database connection state by calling the API and updating the state provider.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DatabaseConnectionManager"/> class.
/// </remarks>
/// <param name="httpClient">The HTTP client for API calls.</param>
/// <param name="dbStateProvider">The state provider to update.</param>
public class DatabaseConnectionManager : IDatabaseConnectionManager
{
    #region Fields
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory? _httpClientFactory;

    private readonly IDatabaseConnectionStateProvider _stateProvider;
    #endregion
    public DatabaseConnectionManager(
        IHttpClientFactory httpClientFactory,
        IDatabaseConnectionStateProvider stateProvider
        )
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _httpClient = _httpClientFactory.CreateClient("SlottetApi") ?? throw new InvalidOperationException("Failed to create HttpClient.");
        _stateProvider = stateProvider ?? throw new ArgumentNullException(nameof(stateProvider));
    }

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
