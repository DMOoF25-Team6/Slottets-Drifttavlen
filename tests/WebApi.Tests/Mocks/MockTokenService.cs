// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Services;

using Domain.Entities;

namespace WebApi.Tests.Mocks;

/// <summary>
/// Mock implementation of ITokenService for integration tests.
/// </summary>
public class MockTokenService : ITokenService
{

    public string GenerateToken(User user, IList<string> roles, IList<System.Security.Claims.Claim>? roleClaims = null)
        => "test-jwt-token";

    public static string GenerateRefreshToken()
        => "test-refresh-token";

    public static bool ValidateToken(string token, out string? userId)
    {
        userId = "test-user-id";
        return token == "test-jwt-token";
    }
}
