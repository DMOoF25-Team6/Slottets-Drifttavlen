// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.


using Core.Interfaces.Providers;
using Core.Interfaces.Services;
using Core.Providers;
using Core.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        // Domain entities services
        _ = services.AddScoped<IResidentService, ResidentService>();
        _ = services.AddScoped<IResidentNoteService, ResidentNoteService>();
        _ = services.AddScoped<IMedicineStatusService, MedicineStatusService>();
        _ = services.AddScoped<IPhoneAssignmentService, PhoneAssignmentService>();
        _ = services.AddScoped<IAccountService, AccountService>();

        // Other services
        _ = services.AddScoped<IDatabaseConnectionService, DatabaseConnectionService>();

        // Register the TokenService for handling JWT token generation and validation
        _ = services.AddScoped<ITokenService, TokenService>();

        // Register the DatabaseConnectionStateProvider as a singleton to maintain a single instance across the application
        _ = services.AddSingleton<IDatabaseConnectionStateProvider, DatabaseConnectionStateProvider>();

        return services;
    }
}
