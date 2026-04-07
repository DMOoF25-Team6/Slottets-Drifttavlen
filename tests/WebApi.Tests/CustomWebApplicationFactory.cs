// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Infrastructure.Data.Persistent;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace WebApi.Tests;

/// <summary>
/// Provides a custom <see cref="WebApplicationFactory{TStartup}"/> for integration testing with an in-memory database.
/// </summary>
/// <typeparam name="TStartup">The entry point class of the application under test.</typeparam>
/// <remarks>
/// This factory replaces the application's <see cref="AppDbContext"/> registration with an in-memory database for test isolation.
/// </remarks>
/// <example>
/// <code language="csharp">
/// using var factory = new CustomWebApplicationFactory<Program>();
/// var client = factory.CreateClient();
/// </code>
/// </example>
public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    /// <summary>
    /// Configures the web host to use an in-memory database for integration tests.
    /// </summary>
    /// <param name="builder">A web host builder for configuring the test server.</param>
    /// <remarks>
    /// Removes the existing <see cref="AppDbContext"/> registration and replaces it with an in-memory database.
    /// Ensures the database is created before tests run.
    /// </remarks>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _ = builder.ConfigureServices(services =>
        {
            // Remove the app's DbContext registration
            ServiceDescriptor? descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor is not null)
            {
                _ = services.Remove(descriptor);
            }

            // Add in-memory database
            _ = services.AddDbContext<AppDbContext>(options =>
            {
                _ = options.UseInMemoryDatabase("TestDb");
            });

            // Ensure database is created
            ServiceProvider sp = services.BuildServiceProvider();
            using IServiceScope scope = sp.CreateScope();
            AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            _ = db.Database.EnsureCreated();
        });
    }
}
