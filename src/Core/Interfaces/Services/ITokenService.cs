// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

namespace Core.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user, IList<string> roles);
}
