// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Services;

using Domain.Entities;

using Infrastructure.Data.Persistent;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

/// <summary>
/// Provides persistent storage for refresh tokens using Entity Framework Core.
/// </summary>
public class RefreshTokenStore(AppDbContext dbContext) : IRefreshTokenStore
{
    private readonly AppDbContext dbContext = dbContext;

    public async Task SaveAsync(RefreshToken token, CancellationToken cancellationToken = default)
    {
        // Try to find existing token by Id (primary key) or TokenHash value
        RefreshToken? existing = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Id == token.Id || rt.TokenHash == token.TokenHash, cancellationToken);
        if (existing is not null)
        {
            // Update properties
            existing.RevokedAt = token.RevokedAt;
            existing.RevokedReason = token.RevokedReason;
            existing.ExpiresAt = token.ExpiresAt;
            existing.CreatedAt = token.CreatedAt;
            existing.CreatedByIp = token.CreatedByIp;
            // ...add any other fields that may change
            _ = dbContext.RefreshTokens.Update(existing);
        }
        else
        {
            _ = await dbContext.RefreshTokens.AddAsync(token, cancellationToken);
        }
        _ = await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        string tokenHash = RefreshToken.ComputeSha256Hash(token);
        return await dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash, cancellationToken);
    }

    public async Task RevokeAsync(string token, CancellationToken cancellationToken = default)
    {
        string tokenHash = RefreshToken.ComputeSha256Hash(token);
        RefreshToken? entity = await dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash, cancellationToken);
        if (entity is not null)
        {
            _ = dbContext.RefreshTokens.Remove(entity);
            _ = await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
