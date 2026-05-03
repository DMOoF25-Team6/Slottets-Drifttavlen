// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Services;

using Infrastructure.Data.Persistent;

namespace Infrastructure.Data.Services;

/// <summary>
/// Provides database connectivity status operations for the application.
/// </summary>
/// <remarks>
/// This service uses <see cref="AppDbContext"/> to check the database connection status.
/// </remarks>
/// <remarks>
/// Initializes a new instance of the <see cref="DatabaseService"/> class.
/// </remarks>
/// <param name="dbContext">An instance of the application's database context.</param>
public class DatabaseService(AppDbContext dbContext) : IDatabaseService
{

    /// <summary>
    /// Determines whether the application can connect to the configured database.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if the database connection is available; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Returns <see langword="false"/> if an exception occurs during the connection check.
    /// </remarks>
    public bool IsConnected()
    {
        try
        {
            return dbContext.Database.CanConnect();
        }
        catch
        {
            return false;
        }
    }
}
