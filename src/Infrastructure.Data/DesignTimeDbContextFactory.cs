// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Infrastructure.Data.Persistent;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data;

/// <summary>
/// Provides a design-time factory for AppDbContext to enable EF Core tools to create migrations
/// using the same MySQL connection as production, reading from environment variables.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();

        // Read connection string template from environment or fallback
            var connStr = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__APPDBCONTEXT")
                ?? "server={DB_HOST};port=3307;database={DB_NAME};user={DB_USER};password={DB_PASSWORD};SslMode=none;";

        string dbUser = Environment.GetEnvironmentVariable("MYSQL_USER") ?? throw new InvalidOperationException("MYSQL_USER environment variable not found.");
        string dbPass = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? throw new InvalidOperationException("MYSQL_PASSWORD environment variable not found.");
        string dbHost = Environment.GetEnvironmentVariable("MYSQL_HOST") ?? throw new InvalidOperationException("MYSQL_HOST environment variable not found.");
        string dbName = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? throw new InvalidOperationException("MYSQL_DATABASE environment variable not found.");

        connStr = connStr.Replace("{DB_USER}", dbUser)
                         .Replace("{DB_PASSWORD}", dbPass)
                         .Replace("{DB_HOST}", dbHost)
                         .Replace("{DB_NAME}", dbName);

        _ = optionsBuilder.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
        return new AppDbContext(optionsBuilder.Options);
    }
}
