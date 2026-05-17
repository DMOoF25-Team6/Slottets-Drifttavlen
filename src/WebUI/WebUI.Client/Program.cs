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

        // Register infrastructure (HttpClient "SlottetApi" + managers + services).
        _ = builder.Services.AddInfrastructure(builder.Configuration);

        // Token storage and authentication services (client-side).
        _ = builder.Services.AddScoped<TokenStorageService>();
        _ = builder.Services.AddScoped<JwtAuthorizationMessageHandler>();
        _ = builder.Services.AddScoped<AuthService>();

        // The "CanManageResidents" policy allows access to resident management features for users with either:
        //   - the "admin" role, or
        //   - a "CanManageResidents" role claim (for more granular control without granting full admin privileges).
        _ = builder.Services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("CanManageResidents", policy =>
                policy.RequireAssertion(ctx =>
                    ctx.User.IsInRole("admin") ||
                    ctx.User.HasClaim(c => c.Type == System.Security.Claims.ClaimTypes.Role && c.Value == "CanManageResidents")));
        });

        _ = builder.Services.AddCascadingAuthenticationState();
        _ = builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();

        // UC-007: register the JWT message handlers and wire them into the
        // "SlottetApi" HttpClient pipeline.
        //
        // Pipeline order (outermost first):
        //   1. JwtRefreshMessageHandler  — intercepts 401 responses, refreshes the
        //                                   access token, and retries the original request.
        //   2. JwtAuthorizationMessageHandler — attaches "Authorization: Bearer <token>"
        //                                        to every outgoing request.
        //
        // The order matters: if Refresh ran INSIDE Authorize, a refreshed token
        // would not be reattached on retry. Refresh outside Authorize ensures the
        // retried request goes through Authorize again and picks up the new token.
        _ = builder.Services.AddTransient<JwtAuthorizationMessageHandler>();
        _ = builder.Services.AddTransient<JwtRefreshMessageHandler>();

        _ = builder.Services.AddHttpClient("SlottetApi")
            .AddHttpMessageHandler<JwtRefreshMessageHandler>()
            .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

        await builder.Build().RunAsync();
    }
}
