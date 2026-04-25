// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Security.Claims;

using Domain.Entities;


namespace Api.Helpers;

/// <summary>
/// Extension methods for WebApplication.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Adds the Identity API endpoints with custom registration authorization.
    /// </summary>
    /// <param name="app">The WebApplication instance.</param>
    public static IEndpointConventionBuilder AddEndpointIdentityApi(this WebApplication app)
    {
        // Suppress the default /register endpoint so only custom registration is allowed
        IEndpointConventionBuilder identityApis = app.MapIdentityApi<User>();

        _ = identityApis.AddEndpointFilter(async (context, next) =>
        {
            if (context.HttpContext.Request.Path.StartsWithSegments("/register", StringComparison.OrdinalIgnoreCase))
            {
                ClaimsPrincipal user = context.HttpContext.User;

                // Check if the user is authenticated and has the specific claim
                if (!user.Identity?.IsAuthenticated == true ||
                    !user.HasClaim(c => c.Type == "CanManageUsers"))
                {
                    return Results.Forbid();
                }
            }
            return await next(context);
        });
        return identityApis;
    }
}
