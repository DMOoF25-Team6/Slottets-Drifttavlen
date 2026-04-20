// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;

//namespace Infrastructure.Persistent;

///// <summary>
///// Design-time factory for AppDbContext to support EF Core CLI tools.
///// Loads connection string from .env, appsettings.Development.json, or environment variables.
///// </summary>
//public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
//{
//    public AppDbContext CreateDbContext(string[] args)
//    {
//        // Build configuration
//        string basePath = Directory.GetCurrentDirectory();
//        IConfigurationBuilder builder = new ConfigurationBuilder()
//            .SetBasePath(basePath)
//            .AddJsonFile("appsettings.json", optional: true)
//            .AddJsonFile("appsettings.Development.json", optional: true)
//            .AddEnvironmentVariables();

//        // Optionally load .env if present
//        string envPath = Path.Combine(basePath, ".env");
//        if (File.Exists(envPath))
//        {
//            foreach (string line in File.ReadAllLines(envPath))
//            {
//                string trimmed = line.Trim();
//                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith("#")) continue;
//                int idx = trimmed.IndexOf('=');
//                if (idx > 0)
//                {
//                    string key = trimmed[..idx].Trim();
//                    string value = trimmed[(idx + 1)..].Trim();
//                    Environment.SetEnvironmentVariable(key, value);
//                }
//            }
//        }

//        IConfigurationRoot config = builder.Build();

//        // 1. Try environment variable (matches runtime)
//        string? connectionString = Environment.GetEnvironmentVariable("ConnectionString__AppDbContext");

//        // 2. Fallback to config file (standard .NET pattern)
//        if (string.IsNullOrWhiteSpace(connectionString))
//        {
//            connectionString = config.GetConnectionString("AppDbContext");
//        }

//        if (string.IsNullOrWhiteSpace(connectionString))
//        {
//            throw new InvalidOperationException($"Connection string for AppDbContext not found in environment variables or configuration files.");
//        }

//        // Replace placeholders with environment variables if present
//        if (connectionString.Contains("{DB_USER}") || connectionString.Contains("{DB_PASSWORD}") || connectionString.Contains("{DB_HOST}") || connectionString.Contains("{DB_NAME}"))
//        {
//            string dbUser = Environment.GetEnvironmentVariable("MYSQL_USER") ?? throw new InvalidOperationException("MYSQL_USER environment variable not found.");
//            string dbPass = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? throw new InvalidOperationException("MYSQL_PASSWORD environment variable not found.");
//            string dbHost = Environment.GetEnvironmentVariable("MYSQL_HOST") ?? throw new InvalidOperationException("MYSQL_HOST environment variable not found.");
//            string dbName = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? throw new InvalidOperationException("MYSQL_DATABASE environment variable not found.");
//            connectionString = connectionString.Replace("{DB_USER}", dbUser)
//                                               .Replace("{DB_PASSWORD}", dbPass)
//                                               .Replace("{DB_HOST}", dbHost)
//                                               .Replace("{DB_NAME}", dbName);
//        }

//        DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();
//        _ = optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//        return new AppDbContext(optionsBuilder.Options);
//    }
//}
