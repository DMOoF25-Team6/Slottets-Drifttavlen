// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Enums;

namespace Domain.UiModels;

/// <summary>
/// Represents the current state of the data connection for UI purposes.
/// Not a database entity.
/// </summary>
public class DataConnection
{
    /// <summary>
    /// The current state (e.g., Online, Offline, Reconnecting, Unknown).
    /// </summary>
    /// <summary>
    /// The current state (e.g., Online, Offline, Reconnecting, Unknown).
    /// </summary>
    public DbConnectionState State { get; set; }

    /// <summary>
    /// The last time the connection was checked.
    /// </summary>
    public DateTime? LastChecked { get; set; }

    /// <summary>
    /// Error message if the connection state is error/unknown.
    /// </summary>
    public required string ErrorMessage { get; set; }
}
