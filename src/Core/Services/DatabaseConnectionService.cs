// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Managers;
using Core.Interfaces.Services;

namespace Core.Services;

public class DatabaseConnectionService : IDatabaseConnectionService
{
    private readonly IDatabaseConnectionManager _databaseConnectionManager;
    private readonly System.Threading.Timer _timer;
    private bool _disposed;

    public DatabaseConnectionService(IDatabaseConnectionManager databaseConnectionManager)
    {
        _databaseConnectionManager = databaseConnectionManager ?? throw new ArgumentNullException(nameof(databaseConnectionManager));

        // Start the timer to check connection every 30 seconds
        _timer = new System.Threading.Timer(
            async _ => await CheckDatabaseConnectionAsync(),
            null,
            TimeSpan.Zero, // Start immediately
            TimeSpan.FromSeconds(30)); // Repeat every 30 seconds
    }

    public async Task<bool> CheckDatabaseConnectionAsync()
    {
        return await _databaseConnectionManager.CheckAndUpdateConnectionStateAsync();
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _timer?.Dispose();
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
