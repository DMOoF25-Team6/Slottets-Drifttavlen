// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Persistent;

/// <summary>
/// Provides static seed data for Identity roles and claims.
/// </summary>
public static class IdentityRoleClaimSeed
{
    /// <summary>
    /// Seeds example roles and claims using EF Core's HasData.
    /// Call from OnModelCreating in your DbContext.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    public static void Seed(ModelBuilder modelBuilder)
    {
        Guid adminRoleId = Guid.Parse("fabc2277-7992-491b-ae4a-bc78f8de56aa");
        Guid superUser = Guid.Parse("d1c9e8b5-3f4a-4c2e-9a1b-5e6f7a8b9c0d");
        Guid userRoleId = Guid.Parse("ee697c76-947a-4fe2-8b14-40194c30bdae");
        _ = modelBuilder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid>
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole<Guid>
            {
                Id = superUser,
                Name = "SuperUser",
                NormalizedName = "SUPERUSER"
            },
            new IdentityRole<Guid>
            {
                Id = userRoleId,
                Name = "User",
                NormalizedName = "USER"
            }
        );

        _ = modelBuilder.Entity<IdentityRoleClaim<Guid>>().HasData(
            new IdentityRoleClaim<Guid>
            {
                Id = 1,
                RoleId = adminRoleId,
                ClaimType = System.Security.Claims.ClaimTypes.Role,
                ClaimValue = "CanManageUsers"
            },
            new IdentityRoleClaim<Guid>
            {
                Id = 2,
                RoleId = adminRoleId,
                ClaimType = System.Security.Claims.ClaimTypes.Role,
                ClaimValue = "CanManageMedicine"
            },
            new IdentityRoleClaim<Guid>
            {
                Id = 3,
                RoleId = userRoleId,
                ClaimType = System.Security.Claims.ClaimTypes.Role,
                ClaimValue = "CanViewMedicine"
            },
            new IdentityRoleClaim<Guid>
            {
                Id = 4,
                RoleId = userRoleId,
                ClaimType = System.Security.Claims.ClaimTypes.Role,
                ClaimValue = "CanManageResidents"
            }
        );
    }
}
