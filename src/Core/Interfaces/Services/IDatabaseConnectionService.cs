// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Core.Interfaces.Services;

public interface IDatabaseConnectionService
{
    Task CheckDatabaseConnectionAsync();
}