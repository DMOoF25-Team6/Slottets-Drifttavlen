// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


// For SwaggerGen extension methods

using Infrastructure.Data;
using Infrastructure.Data.Persistent;

using Microsoft.EntityFrameworkCore;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        // Load environment variables from .env file
        _ = DotNetEnv.Env.Load(AppContext.BaseDirectory);

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Replace the connection string params with the one from the environment variable
        ConfigurationManager conf = builder.Configuration;
        string connectionString = DbContextConfiguration(builder, conf);
        // Register both DbContext and DbContextFactory for DI
        _ = builder.Services.AddDbContext<AppDbContext>(options =>
        {
            _ = options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        //_ = builder.Services.AddDbContextFactory<AppDbContext>();

        _ = builder.Services.AddInfrastructureData();

        // Add services to the container.
        _ = builder.Services.AddControllers();
        _ = builder.Services.AddSwaggerGen();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }

        _ = app.UseHttpsRedirection();

        _ = app.UseAuthorization();

        _ = app.MapControllers();

        app.Run();
    }

    public static string DbContextConfiguration(WebApplicationBuilder builder, ConfigurationManager conf)
    {
        string? connStr = conf.GetConnectionString("AppDbContext");
        if (!string.IsNullOrEmpty(connStr))
        {
            string dbUser = Environment.GetEnvironmentVariable("MYSQL_USER") ?? throw new InvalidOperationException("MYSQL_USER environment variable not found.");
            string dbPass = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? throw new InvalidOperationException("MYSQL_PASSWORD environment variable not found.");
            string dbHost = Environment.GetEnvironmentVariable("MYSQL_HOST") ?? throw new InvalidOperationException("MYSQL_HOST environment variable not found.");
            string dbName = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? throw new InvalidOperationException("MYSQL_DATABASE environment variable not found.");
            //string dbUser = Environment.GetEnvironmentVariable("MYSQL_USER") ?? string.Empty;
            //string dbPass = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? string.Empty;
            //string dbHost = Environment.GetEnvironmentVariable("MYSQL_HOST") ?? string.Empty;
            //string dbName = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? string.Empty;
            connStr = connStr.Replace("{DB_USER}", dbUser)
                             .Replace("{DB_PASSWORD}", dbPass)
                             .Replace("{DB_HOST}", dbHost)
                             .Replace("{DB_NAME}", dbName);
            builder.Configuration["ConnectionStrings:AppDbContext"] = connStr;
            return connStr;
        }
        throw new InvalidOperationException("Connection string for AppDbContext not found in environment variables.");
    }
}
