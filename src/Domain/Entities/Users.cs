// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.ComponentModel.DataAnnotations;

using Domain.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<Guid>, IEntity
{
    /// <summary>
    /// Gets or sets the user name for this user. Always stored in lowercase for consistency.
    /// </summary>
    [ProtectedPersonalData]
    [EmailAddress]
    public override string? UserName
    {
        get => _userName;
        set => _userName = value?.ToLowerInvariant();
    }

    private string? _userName;
}
