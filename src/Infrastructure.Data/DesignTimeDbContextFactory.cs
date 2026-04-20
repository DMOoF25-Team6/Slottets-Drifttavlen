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
    /// <summary>
    /// Creates a new <see cref="AppDbContext"/> using environment variables for configuration.
    /// Throws <see cref="InvalidOperationException"/> if required variables are missing.
    /// </summary>
    public AppDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();

        // Get connection string template from environment or fallback
        string connStr = Environment.GetEnvironmentVariable("CONNECTIONSTRINGS__APPDBCONTEXT")
            ?? "server={DB_HOST};port=3307;database={DB_NAME};user={DB_USER};password={DB_PASSWORD};SslMode=none;";

        // Helper to read required environment variable or throw
        static string GetEnv(string key)
            => Environment.GetEnvironmentVariable(key)
                ?? throw new InvalidOperationException($"{key} environment variable not found.");

        string dbUser = GetEnv("MYSQL_USER");
        string dbPass = GetEnv("MYSQL_PASSWORD");
        string dbHost = GetEnv("MYSQL_HOST");
        string dbName = GetEnv("MYSQL_DATABASE");

        // Replace placeholders in connection string
        connStr = connStr.Replace("{DB_USER}", dbUser)
                         .Replace("{DB_PASSWORD}", dbPass)
                         .Replace("{DB_HOST}", dbHost)
                         .Replace("{DB_NAME}", dbName);

        // Use MySQL provider with auto-detected server version
        _ = optionsBuilder.UseMySql(connStr, ServerVersion.AutoDetect(connStr));

        return new AppDbContext(optionsBuilder.Options);
    }
}
