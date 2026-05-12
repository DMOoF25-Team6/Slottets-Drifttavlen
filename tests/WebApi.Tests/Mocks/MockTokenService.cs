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

    public Task<string> CreateJwtTokenAsync(User user, IList<string> roles, IList<System.Security.Claims.Claim> permissions, CancellationToken cancellationToken = default)
        => Task.FromResult("test-jwt-token");

    public Task<RefreshToken> CreateRefreshTokenAsync(User user, string ipAddress, CancellationToken cancellationToken = default)
    {
        const string refreshTokenValue = "test-refresh-token";
        RefreshToken refreshToken = new()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            TokenHash = $"hash-{refreshTokenValue}", // Must match ComputeSha256HashAsync
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,
            CreatedByIp = ipAddress,
            User = user
        };
        // Optionally, if your system expects to return the raw token, you may need to expose it.
        return Task.FromResult(refreshToken);
    }

    public Task<string> ComputeSha256HashAsync(string token, CancellationToken cancellationToken = default)
    {
        // Return a deterministic fake hash for testing
        return Task.FromResult($"hash-{token}");
    }
}
