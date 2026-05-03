// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.Interfaces.Providers;

/// <summary>
/// Abstraction for database connection state provider.
/// </summary>
public interface IDatabaseConnectionStateProvider
{
    /// <summary>
    /// Gets a value indicating whether the database is connected.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Sets the connection state.
    /// </summary>
    /// <param name="isConnected">True if connected, otherwise false.</param>
    void SetConnectionState(bool isConnected);

    /// <summary>
    /// Event triggered when the connection state changes.
    /// </summary>
    event Action? StateChanged;
}
