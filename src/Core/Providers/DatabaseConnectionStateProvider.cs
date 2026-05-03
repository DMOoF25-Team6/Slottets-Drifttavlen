// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Providers;

namespace Core.Providers;

/// <summary>
/// Provides the state of the database connection.
/// </summary>
public class DatabaseConnectionStateProvider : IDatabaseConnectionStateProvider
{
    public event Action? StateChanged;

    // Do not use auto property to avoid unnecessary event invocations
#pragma warning disable IDE0032 // Use auto property
    private bool _isConnected;
#pragma warning restore IDE0032 // Use auto property

    public bool IsConnected
    {
        get => _isConnected;
        private set
        {
            if (_isConnected != value)
            {
                _isConnected = value;
                StateChanged?.Invoke();
            }
        }
    }

    public void SetConnectionState(bool isConnected)
    {
        IsConnected = isConnected;
    }
}
