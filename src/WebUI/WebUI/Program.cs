// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core;

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

        // Add services to the container.
        _ = builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        _ = builder.Services.AddCore();
        _ = builder.Services.AddInfrastructure();



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

        _ = app.UseHttpsRedirection();

        _ = app.UseStaticFiles();
        _ = app.UseAntiforgery();

        _ = app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.Run();
    }
}
