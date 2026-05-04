// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;
using Core.Services;

using Infrastructure.Managers;
using Infrastructure.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Registers infrastructure services and configures the database context using the application's configuration.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The application configuration, used to retrieve the connection string.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddCore();
        string? apiBaseUrl = configuration["WebApiHostAddress"];
        if (string.IsNullOrWhiteSpace(apiBaseUrl))
        {
            throw new InvalidOperationException("WebApiHostAddress is not configured.");
        }
        _ = services.AddHttpClient("SlottetApi", client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl.EndsWith('/') ? apiBaseUrl : apiBaseUrl + "/");
        });

        // CreateAccountAsync managers that contain business logic and coordinate between repositories and services
        _ = services.AddScoped<IResidentManager, ResidentManager>();
        _ = services.AddScoped<IAccountManager, AccountManager>();
        _ = services.AddScoped<IPhoneAssignmentManager, PhoneAssignmentManager>();
        _ = services.AddScoped<IResidentNoteManager, ResidentNoteManager>();
        _ = services.AddScoped<IMedicineStatusManager, MedicineStatusManager>();

        // Audit service for logging actions, errors, and important events to the database
        _ = services.AddScoped<IAuditService, AuditService>();

        // CreateAccountAsync TokenService for ITokenService
        _ = services.AddScoped<ITokenService, TokenService>();

        _ = services.AddScoped<IDatabaseConnectionManager, DatabaseConnectionManager>();

        return services;
    }
}
