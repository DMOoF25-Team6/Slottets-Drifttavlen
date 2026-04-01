// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core;
using Core.Interfaces.Managers;
using Core.Interfaces.Repositories;

using Infrastructure.Persistent;
using Infrastructure.Repositories;
using Infrastructure.Services;

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
        _ = services.AddScoped<IMedicineRepository, MedicineRepository>();
        _ = services.AddScoped<IPainkillerRepository, PainKillerRepository>();

        services.AddHttpClient<IResidentManager, ResidentManager>();
        services.AddHttpClient<IMedicineRecordManager, MedicineRecordManager>();

        _ = services.AddDbContextFactory<AppDbContext>(options =>
        {
            string connectionString = Environment.GetEnvironmentVariable("DefaultConnection") ?? throw new InvalidOperationException($"Connection string '{nameof(AppDbContext)}' not found.");
            _ = options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        return services;
    }
}
