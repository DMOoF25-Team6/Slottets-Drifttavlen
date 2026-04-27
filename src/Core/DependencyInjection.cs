// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.


using Core.Interfaces.Services;
using Core.Services;

using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        _ = services.AddScoped<IResidentService, ResidentService>();
        _ = services.AddScoped<IResidentNoteService, ResidentNoteService>();

        _ = services.AddScoped<IMedicineStatusService, MedicineStatusService>();
        //_ = services.AddScoped<IMedicineRecordService, MedicineRecordService>(); // This service is currently not implemented, so it's commented out to avoid confusion.
        // _ = services.AddScoped<IPainkillerRecordService, PainkillerRecordService>(); // This service is currently not implemented, so it's commented out to avoid confusion.
        _ = services.AddScoped<IAccountService, AccountService>();
       

        _ = services.AddScoped<IPhoneAssignmentService, PhoneAssignmentService>();
        _ = services.AddScoped<IAccountService, AccountService>();


        return services;
    }
}
