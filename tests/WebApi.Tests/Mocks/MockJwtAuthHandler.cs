// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Security.Claims;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApi.Tests.Mocks;

/// <summary>
/// Authentication handler that accepts the test token and sets admin identity for integration tests.
/// </summary>
public class MockJwtAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    [Obsolete("This constructor is required for the authentication handler. It is not intended to be used directly.")]
    public MockJwtAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Accept the test token as valid and set admin claims
        string authHeader = Request.Headers.Authorization.ToString();
        if (authHeader == "Bearer test-jwt-token")
        {
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, "admin@example.com"),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        return Task.FromResult(AuthenticateResult.Fail("Invalid token"));
    }
}
