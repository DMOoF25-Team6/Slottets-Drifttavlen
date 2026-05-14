// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

using Domain.Entities;

using Infrastructure.Data.Repositories;
using Infrastructure.Services;

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
        _ = services.AddScoped<IPhoneAssignmentRepository, PhoneAssignmentRepository>();
       _  = services.AddScoped<IStaffAssignmentRepository, StaffAssignmentRepository>();
        _ = services.AddScoped<IAuditRepository, AuditRepository>();
       _ =  services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        // Identity services
        _ = services.AddScoped<IRefreshTokenStore, RefreshTokenStore>();

        // Database connectivity service
        _ = services.AddScoped<IDatabaseService, Services.DatabaseService>();

        // Staff assignment services
        _ = services.AddScoped<IStaffAssignmentService, StaffAssignmentService>();

        //
        _ = services.AddKeyedScoped<IEnumerable<AuditEntry>>("Audit", (_, _) => []);

        return services;
    }
}
