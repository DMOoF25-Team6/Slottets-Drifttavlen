// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Infrastructure.Data.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data;

public static class DependencyInjection
{
    /// <summary>
    /// Registers infrastructure data services and provides a method to seed Identity roles and claims.
    /// </summary>
    public static IServiceCollection AddInfrastructureData(this IServiceCollection services)
    {
        _ = services.AddScoped<IResidentRepository, ResidentRepository>();
        _ = services.AddScoped<IResidentNoteRepository, ResidentNoteRepository>();
        _ = services.AddScoped<IMedicineRepository, MedicineRepository>();
        _ = services.AddScoped<IPainkillerRepository, PainKillerRepository>();
        _ = services.AddScoped<IAuditRepository, AuditRepository>();
        _ = services.AddScoped<IPhoneAssignmentRepository, PhoneAssignmentRepository>();
        _ = services.AddScoped<IAuditRepository, AuditRepository>();

        // NOTE: To seed Identity roles and claims, call IdentitySeed.SeedRolesAndClaimsAsync(serviceProvider) at startup.

        return services;
    }
}

