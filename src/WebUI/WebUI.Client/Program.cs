// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Infrastructure;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using WebUI.Client.Services;

namespace WebUI.Client;

internal class Program
{
    private static async Task Main(string[] args)
    {
        WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
        _ = builder.Services.AddInfrastructure(builder.Configuration);
        _ = builder.Services.AddScoped<TokenStorageService>();
        _ = builder.Services.AddScoped<AuthService>();
        _ = builder.Services.AddAuthorizationCore();
        _ = builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();

        await builder.Build().RunAsync();
    }
}
