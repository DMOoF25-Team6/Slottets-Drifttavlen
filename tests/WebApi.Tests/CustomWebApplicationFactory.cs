// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.Interfaces.Services;

using Domain.Entities;

using Infrastructure.Data.Persistent;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using WebApi.Tests.Mocks;

namespace WebApi.Tests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("TokenValidationParameters__IssuerSigningKey", "TestSecretKey12345678901234567890");

        _ = builder.ConfigureAppConfiguration((context, config) =>
        {
            _ = config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["TokenValidationParameters:Issuer"] = "http://localhost",
                ["TokenValidationParameters:Audience"] = "http://localhost",
                ["TokenValidationParameters:IssuerSigningKey"] = "TestSecretKey12345678901234567890",
                ["TokenValidationParameters__IssuerSigningKey"] = "TestSecretKey12345678901234567890"
            });
        });

        _ = builder.ConfigureServices(services =>
        {
            _ = services.AddScoped<IPhoneAssignmentService, MockPhoneAssignmentService>();
                // CreateAccountAsync MockTokenService for ITokenService
                _ = services.AddScoped<ITokenService, WebApi.Tests.Mocks.MockTokenService>();

            // Override authentication for integration tests
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "TestScheme";
                options.DefaultChallengeScheme = "TestScheme";
            })
            .AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, WebApi.Tests.Mocks.MockJwtAuthHandler>(
                "TestScheme", options => { });

            ConfigureTestDatabase(services);

            ServiceProvider sp = services.BuildServiceProvider();
            using IServiceScope scope = sp.CreateScope();
            AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            _ = db.Database.EnsureCreated();

            // Seed admin role and user for tests
            UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole<Guid>> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            string adminRole = EnsureAdminRoleExists(roleManager);

            EnsureAdminUserExists(userManager, adminRole);
        });
    }



    #region Helpers

    private static void ConfigureTestDatabase(IServiceCollection services)
    {
        // Remove any existing AppDbContext registration (including from main app)
        ServiceDescriptor? descriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
        if (descriptor is not null)
        {
            _ = services.Remove(descriptor);
        }
        // Use a unique in-memory database name per test instance to ensure isolation
        string dbName = $"TestDb_{Guid.NewGuid()}";
        _ = services.AddDbContext<AppDbContext>(options =>
        {
            _ = options.UseInMemoryDatabase(dbName);
        });
    }

    private static string EnsureAdminRoleExists(RoleManager<IdentityRole<Guid>> roleManager)
    {
        // Ensure admin role exists
        const string adminRole = "admin";
        if (!roleManager.RoleExistsAsync(adminRole).Result)
        {
            _ = roleManager.CreateAsync(new IdentityRole<Guid>(adminRole));
        }

        return adminRole;
    }

    private static void EnsureAdminUserExists(UserManager<User> userManager, string adminRole)
    {
        // Ensure admin user exists
        const string adminEmail = "admin@example.com";
        const string adminPassword = "Password123!";
        User? adminUser = userManager.FindByEmailAsync(adminEmail).Result;
        if (adminUser is null)
        {
            adminUser = new User { UserName = adminEmail, Email = adminEmail };
            _ = userManager.CreateAsync(adminUser, adminPassword).GetAwaiter().GetResult();
            _ = userManager.AddToRoleAsync(adminUser, adminRole).GetAwaiter().GetResult();
        }
    }
    #endregion
}
