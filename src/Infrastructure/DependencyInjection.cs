// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Managers;

using Infrastructure.Services;

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
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        _ = services.AddHttpClient<IResidentManager, ResidentManager>();
        // _ = services.AddHttpClient<IResidentNoteManager, ResidentNoteManager>(); // TODO: Implement ResidentNoteManager
        // _ = services.AddHttpClient<IPainkillerRecordManager, PainkillerRecordManager>(); // TODO: Implement PainkillerRecordManager
        // _ = services.AddHttpClient<IMedicineRecordManager, MedicineRecordManager>(); // TODO: Implement MedicineRecordManager
        _ = services.AddHttpClient<IAccountManager, AccountManager>();

        return services;
    }
}
