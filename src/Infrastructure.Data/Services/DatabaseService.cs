// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Services;

using Infrastructure.Data.Persistent;

namespace Infrastructure.Data.Services;

public class DatabaseService : IDatabaseService
{
    private readonly AppDbContext _dbContext;

    public DatabaseService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool IsConnected()
    {
        try
        {
            return _dbContext.Database.CanConnect();
        }
        catch
        {
            return false;
        }
    }
}
