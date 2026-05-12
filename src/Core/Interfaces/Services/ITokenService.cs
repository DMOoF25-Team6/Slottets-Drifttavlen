// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

namespace Core.Interfaces.Services;

public interface ITokenService
{
    Task<string> CreateJwtTokenAsync(User user, IList<string> roles, IList<System.Security.Claims.Claim> permissions);
    Task<RefreshToken> CreateRefreshTokenAsync(User user, string ipAddress);
    Task<string> ComputeSha256Hash(string token);
}
