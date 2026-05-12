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

    public Task<string> CreateJwtTokenAsync(User user, IList<string> roles, IList<System.Security.Claims.Claim> permissions)
        => Task.FromResult("test-jwt-token");

    public Task<RefreshToken> CreateRefreshTokenAsync(User user, string ipAddress)
    {
        const string refreshTokenValue = "test-refresh-token";
        RefreshToken refreshToken = new()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            TokenHash = $"hash-{refreshTokenValue}", // Must match ComputeSha256Hash
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,
            CreatedByIp = ipAddress,
            User = user
        };
        // Optionally, if your system expects to return the raw token, you may need to expose it.
        return Task.FromResult(refreshToken);
    }

    public Task<string> ComputeSha256Hash(string token)
    {
        // Return a deterministic fake hash for testing
        return Task.FromResult($"hash-{token}");
    }
}
