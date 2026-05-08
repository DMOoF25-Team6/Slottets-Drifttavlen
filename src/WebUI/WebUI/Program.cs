// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Security.Claims;
using System.Text;

using Core.Interfaces.Managers;
using Core.Services;

using Infrastructure;
using Infrastructure.Managers;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;

using WebUI.Client;
using WebUI.Client.Services;
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

        _ = builder.Configuration.AddEnvironmentVariables(); // Ensure environment variables are available in configuration

        _ = builder.Services.AddInfrastructure(builder.Configuration);

        // Add services to the container.
        _ = builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        _ = builder.Services.AddCascadingAuthenticationState();

        // Persist Data Protection keys to a directory for antiforgery token decryption across restarts/containers
        _ = builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(dataProtectionKeysDir));

        // And for the interface, e.g.:
        _ = builder.Services.AddScoped<IAccountManager, AccountManager>();

        _ = builder.Services.AddScoped<TokenStorageService>();
        _ = builder.Services.AddScoped<AuthService>();
        _ = builder.Services.AddAuthorizationCore();
        _ = builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
        _ = builder.Services.AddScoped<AccountService>();
        // Set the default authentication and challenge scheme to JwtBearer
        _ = builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // Use configuration values for TokenValidationParameters validation
                string issuer = builder.Configuration["TokenValidationParameters:Issuer"] ?? throw new InvalidOperationException("TokenValidationParameters:Issuer not found in configuration.");
                string audience = builder.Configuration["TokenValidationParameters:Audience"] ?? throw new InvalidOperationException("TokenValidationParameters:Audience not found in configuration.");
                string key = builder.Configuration["TokenValidationParameters:IssuerSigningKey"] ?? throw new InvalidOperationException("TokenValidationParameters  :IssuerSigningKey not found in configuration.");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero
                };
            });


        _ = builder.Services.AddAuthorization();

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

        //_ = app.UseHttpsRedirection();

        _ = app.UseStaticFiles();
        _ = app.UseAntiforgery();

        // Add authentication and authorization middleware
        _ = app.UseAuthentication();
        _ = app.UseAuthorization();

        // CreateAccountAsync JwtRefreshMiddleware before endpoints
        //app.UseMiddleware<WebUI.Middleware.JwtRefreshMiddleware>();

        _ = app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.Run();
    }
}
