// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Infrastructure.Data.Persistent;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Helpers;

public static class DatabaseConnection
{
    public static bool IsConnected(IServiceProvider serviceProvider)
    {
        // get DbContext from DI container
        using IServiceScope scope = serviceProvider.CreateScope();
        AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        try
        {
            _ = dbContext.Database.CanConnect();
        }
        catch
        {
            return false;
        }
        return true;
    }
}
