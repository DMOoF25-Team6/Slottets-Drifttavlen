// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Services;

using Infrastructure;

using Microsoft.AspNetCore.DataProtection;

using WebUI.Components;

namespace WebUI;

public class Program
{
    public static void Main(string[] args)
    {
        // Ensure DataProtection-Keys directory exists for key persistence
        string dataProtectionKeysDir = Path.Combine(AppContext.BaseDirectory, "DataProtection-Keys");
        if (!Directory.Exists(dataProtectionKeysDir))
        {
            _ = Directory.CreateDirectory(dataProtectionKeysDir);
        }

        // Load environment variables from .env file
        _ = DotNetEnv.Env.Load(AppContext.BaseDirectory);

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Replace the connection string params with the one from the environment variable
        ConfigurationManager conf = builder.Configuration;
        string? connStr = conf.GetConnectionString("DefaultConnection");
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
            builder.Configuration["ConnectionStrings:DefaultConnection"] = connStr;
        }

        // Add services to the container.
        _ = builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        _ = builder.Services.AddInfrastructure();

        //Add ResidentService and API client 
        _ = builder.Services.AddScoped<ResidentService>();
        // Persist Data Protection keys to a directory for antiforgery token decryption across restarts/containers
        _ = builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(dataProtectionKeysDir));

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            _ = app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //_ = app.UseHsts();
        }

        // _ = app.UseHttpsRedirection();

        _ = app.UseStaticFiles();
        _ = app.UseAntiforgery();

        _ = app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.Run();
    }
}
