// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core;
using Core.Interfaces.Repositories;

using Infrastructure.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        _ = services.AddCore();
        _ = services.AddScoped<IResidentRepository, ResidentRepository>();
        _ = services.AddScoped<IResidentNoteRepository, ResidentNotesRepository>();
        return services;
    }
}
