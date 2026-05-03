// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.Interfaces.Services;

public interface IDatabaseService
{
    /// <summary>
    /// Checks if the database connection is available.
    /// </summary>
    /// <returns>True if connected, otherwise false.</returns>
    bool IsConnected();
}
