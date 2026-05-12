// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

namespace Core.Interfaces.Services;

/// <summary>
/// Abstraction for refresh token persistence operations.
/// </summary>
public interface IRefreshTokenStore
{
    Task<RefreshToken?> GetByTokenAsync(string token, ITokenService tokenService, CancellationToken cancellationToken = default);
    Task RevokeAsync(string token, ITokenService tokenService, CancellationToken cancellationToken = default);
    Task SaveAsync(RefreshToken token, ITokenService tokenService, CancellationToken cancellationToken = default);
}
