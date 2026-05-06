// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Security.Claims;

using Infrastructure.Data.Persistent;

namespace Api.Middleware;

/// <summary>
/// Middleware that sets the CurrentUserId on AppDbContext from the authenticated user's claims.
/// </summary>
public class CurrentUserMiddleware
{
    private readonly RequestDelegate _next;

    public CurrentUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
    {
        if (context.User.Identity is { IsAuthenticated: true })
        {
            string? userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId, out Guid currentUserId))
            {
                dbContext.CurrentUserId = currentUserId;
            }
        }

        await _next(context);
    }
}
