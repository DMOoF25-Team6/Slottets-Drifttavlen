// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Security.Claims;

using Domain.Entities;

namespace Core.Interfaces.Services;

public interface ITokenService
{
    Task<string> CreateJwtTokenAsync(User user, IList<string> roles, IList<Claim> permissions, CancellationToken cancellationToken = default);
    Task<string> ComputeSha256HashAsync(string token, CancellationToken cancellationToken = default);
    Task<RefreshToken> CreateRefreshTokenAsync(User user, string ipAddress, CancellationToken cancellationToken = default);
}
