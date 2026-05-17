// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Data.Persistent;

namespace Infrastructure.Data.Repositories;

/// <summary>
/// EF Core implementation of <see cref="ILoginAttemptRepository"/> (UC-010).
/// </summary>
public sealed class LoginAttemptRepository(AppDbContext context)
    : Repository<LoginAttempt>(context), ILoginAttemptRepository
{
}
