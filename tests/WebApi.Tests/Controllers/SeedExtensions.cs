// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using Infrastructure.Data.Persistent;

using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Tests.Controllers;

/// <summary>
/// Helpers for seeding the in-memory database backing the integration-test host.
/// The factory exposes the host's service provider via <see cref="Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory{TEntryPoint}.Services"/>,
/// which shares the same in-memory database instance used to serve HTTP requests.
/// </summary>
public static class SeedExtensions
{
    public static void Seed(this CustomWebApplicationFactory<Api.Program> factory, Action<AppDbContext> seed)
    {
        using IServiceScope scope = factory.Services.CreateScope();
        AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        seed(db);
        _ = db.SaveChanges();
    }
}
