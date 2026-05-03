// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Managers;
using Core.Interfaces.Services;

namespace Core.Services;

public class DatabaseConnectionService(IDatabaseConnectionManager databaseConnectionManager) : IDatabaseConnectionService
{
    public async Task CheckDatabaseConnectionAsync()
    {
        await databaseConnectionManager.CheckAndUpdateConnectionStateAsync();
    }
}
