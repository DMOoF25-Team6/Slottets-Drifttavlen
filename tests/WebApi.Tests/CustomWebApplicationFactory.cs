// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Core.Interfaces.Managers;

using Infrastructure.Data.Persistent;

using Microsoft.AspNetCore.Hosting;
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
        Environment.SetEnvironmentVariable("Jwt__SecretKey", "TestSecretKey12345678901234567890");

        _ = builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Jwt:Issuer"] = "http://localhost",
                ["Jwt:Audience"] = "http://localhost",
                ["Jwt:SecretKey"] = "TestSecretKey12345678901234567890",
                ["Jwt__SecretKey"] = "TestSecretKey12345678901234567890"
            });
        });

        _ = builder.ConfigureServices(services =>
        {
            _ = services.AddScoped<IPhoneAssignmentManager, MockPhoneAssignmentManager>();

            ServiceDescriptor? descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor is not null)
            {
                _ = services.Remove(descriptor);
            }

            _ = services.AddDbContext<AppDbContext>(options =>
            {
                _ = options.UseInMemoryDatabase("TestDb");
            });

            ServiceProvider sp = services.BuildServiceProvider();
            using IServiceScope scope = sp.CreateScope();
            AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            _ = db.Database.EnsureCreated();
        });
    }
}
