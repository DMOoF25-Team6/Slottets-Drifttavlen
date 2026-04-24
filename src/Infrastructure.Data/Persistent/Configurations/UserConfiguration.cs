// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Persistent.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
    }

    public static void SeedingData(EntityTypeBuilder<User> builder)
    {
        User superUser = new()
        {
            Id = Guid.Parse("12345678-90AB-CDEF-1234-567890ABCDEF"),
            Email = "superuser@test.test"
        };
        superUser.PasswordHash = PasswordHash(superUser, "Password123!");

        User normalUser = new()
        {
            Id = Guid.Parse("ABCDEF12-3456-7890-ABCD-EF1234567890"),
            Email = "user@test.test"
        };
        normalUser.PasswordHash = PasswordHash(normalUser, "Password123!");

        _ = builder.HasData(superUser, normalUser);
    }

    public static string PasswordHash(User user, string password)
    {
        PasswordHasher<User> passwordHasher = new();
        string hashed = passwordHasher.HashPassword(user, password);
        return hashed;
    }
}
