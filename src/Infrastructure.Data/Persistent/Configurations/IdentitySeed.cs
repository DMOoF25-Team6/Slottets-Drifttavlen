// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Security.Claims;

using Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Persistent.Configurations;

/// <summary>
/// Provides static seed data for Identity roles and claims.
/// </summary>
public static class IdentitySeed
{
    public static readonly User adminUser = new()
    {
        Id = Guid.Parse("3a21f8e1-885b-4394-abf0-ed0baeea239b"),
        UserName = "PederRasmussen@example.com",
        NormalizedUserName = "PEDERRASMUSSEN@EXAMPLE.COM",
        Email = "PederRasmussen@example.com",
        NormalizedEmail = "PEDERRASMUSSEN@EXAMPLE.COM",
        SecurityStamp = Guid.NewGuid().ToString(),
        EmailConfirmed = true
    };
    public static readonly User superUser = new()
    {
        Id = Guid.Parse("4711a300-711e-4132-86d4-cafd3f11deec"),
        UserName = "SanneJohansen@example.com",
        NormalizedUserName = "SANNEJOHANSEN@EXAMPLE.COM",
        Email = "SanneJohansen@example.com",
        NormalizedEmail = "SANNEJOHANSEN@EXAMPLE.COM",
        SecurityStamp = Guid.NewGuid().ToString(),
        EmailConfirmed = true
    };
    public static readonly User normal1User = new()
    {
        Id = Guid.Parse("30cffcf9-5784-4fa9-9c10-c013ef3faf16"),
        UserName = "ThorDanrsøn@example.com",
        NormalizedUserName = "THORDANRSØN@EXAMPLE.COM",
        Email = "ThorDanrsøn@example.com",
        NormalizedEmail = "THORDANRSØN@EXAMPLE.COM",
        SecurityStamp = Guid.NewGuid().ToString(),
        EmailConfirmed = true
    };
    public static readonly User normal2User = new()
    {
        Id = Guid.Parse("37155b80-7111-422a-aba6-89d7070f1644"),
        UserName = "PerNielsen@example.com",
        NormalizedUserName = "PERNIELSEN@EXAMPLE.COM",
        Email = "PerNielsen@example.com",
        NormalizedEmail = "PERNIELSEN@EXAMPLE.COM",
        SecurityStamp = Guid.NewGuid().ToString(),
        EmailConfirmed = true
    };
    public static readonly User normal3User = new()
    {
        Id = Guid.Parse("b836e975-e775-48bc-8b84-5d2bdd5bd87a"),
        UserName = "AndersJensen@example.com",
        NormalizedUserName = "ANDERSJENSEN@EXAMPLE.COM",
        Email = "AndersJensen@example.com",
        NormalizedEmail = "ANDERSJENSEN@EXAMPLE.COM",
        SecurityStamp = Guid.NewGuid().ToString(),
        EmailConfirmed = true
    };
    public static readonly User substitutUser = new()
    {
        Id = Guid.Parse("48245a9c-f2a5-4e8f-9554-b6acc9206d37"),
        UserName = "KasperHolm@example.com",
        NormalizedUserName = "KASPERHOLM@EXAMPLE.COM",
        Email = "KasperHolm@example.com",
        NormalizedEmail = "KASPERHOLM@EXAMPLE.COM",
        SecurityStamp = Guid.NewGuid().ToString(),
        EmailConfirmed = true
    };

    public static readonly IdentityRole<Guid> adminRole = new()
    {
        Id = Guid.Parse("fabc2277-7992-491b-ae4a-bc78f8de56aa"),
        Name = "admin",
        NormalizedName = "ADMIN"
    };
    public static readonly IdentityRole<Guid> superUserRole = new()
    {
        Id = Guid.Parse("d1c9e8b5-3f4a-4c2e-9a1b-5e6f7a8b9c0d"),
        Name = "superuser",
        NormalizedName = "SUPERUSER"
    };
    public static readonly IdentityRole<Guid> careTakerRole = new()
    {
        Id = Guid.Parse("ee697c76-947a-4fe2-8b14-40194c30bdae"),
        Name = "user",
        NormalizedName = "USER"
    };

    public static readonly IdentityRoleClaim<Guid> careTakerClaim1 = new()
    {
        Id = 1,
        RoleId = careTakerRole.Id,
        ClaimType = ClaimTypes.Role,
        ClaimValue = "CanViewMedicine"
    };
    public static readonly IdentityRoleClaim<Guid> adminClaim1 = new()
    {
        Id = 2,
        RoleId = superUserRole.Id,
        ClaimType = ClaimTypes.Role,
        ClaimValue = "CanManageResidents"
    };
    public static readonly IdentityRoleClaim<Guid> superUserClaim1 = new()
    {
        Id = 3,
        RoleId = superUserRole.Id,
        ClaimType = ClaimTypes.Role,
        ClaimValue = "CanViewMedicine"
    };

    public static void UserSeed(ModelBuilder modelBuilder)
    {
        adminUser.PasswordHash = PasswordHash(adminUser, "Password123!");
        superUser.PasswordHash = PasswordHash(superUser, "Password123!");
        normal1User.PasswordHash = PasswordHash(normal1User, "Password123!");
        normal2User.PasswordHash = PasswordHash(normal2User, "Password123!");
        normal3User.PasswordHash = PasswordHash(normal3User, "Password123!");
        substitutUser.PasswordHash = PasswordHash(substitutUser, "Password123!");
        _ = modelBuilder.Entity<User>().HasData(
            adminUser,
            superUser,
            normal1User,
            normal2User,
            normal3User,
            substitutUser);
    }

    /// <summary>
    /// Seeds example roles and claims using EF Core's HasData.
    /// Call from OnModelCreating in your DbContext.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    public static void RolesClaimSeed(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<IdentityRole<Guid>>().HasData(
            adminRole,
            superUserRole,
            careTakerRole);
        _ = modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(
            careTakerClaim1,
            adminClaim1,
            superUserClaim1);
    }
    public static void UserRoleSeed(ModelBuilder modelBuilder)
    {
        // Assign admin user to Admin role
        _ = modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid>
            {
                UserId = adminUser.Id,
                RoleId = adminRole.Id
            }
        );
    }
    public static string PasswordHash(User user, string password)
    {
        PasswordHasher<User> passwordHasher = new();
        string hashed = passwordHasher.HashPassword(user, password);
        return hashed;
    }
}
