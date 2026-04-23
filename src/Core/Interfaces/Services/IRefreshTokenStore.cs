// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

namespace Core.Interfaces.Services;

/// <summary>
/// Abstraction for refresh token persistence operations.
/// </summary>
public interface IRefreshTokenStore
{
    /// <summary>
    /// Saves a refresh token for a user.
    /// </summary>
    Task SaveAsync(RefreshToken token, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a refresh token by token value.
    /// </summary>
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);

    /// <summary>
    /// Revokes a refresh token by token value.
    /// </summary>
    Task RevokeAsync(string token, CancellationToken cancellationToken = default);
}
