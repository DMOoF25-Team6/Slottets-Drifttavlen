// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core;
using Core.Interfaces.Repositories;

using Infrastructure.Persistents;
using Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        _ = services.AddCore();
        _ = services.AddScoped<IResidentRepository, ResidentRepository>();
        _ = services.AddScoped<IResidentNoteRepository, ResidentNoteRepository>();
        _ = services.AddDbContextFactory<AppDbContext>(options =>
        {
            string connectionString = Environment.GetEnvironmentVariable("DefaultConnection") ?? throw new InvalidOperationException($"Connection string '{nameof(AppDbContext)}' not found.");
            _ = options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        return services;
    }
}
