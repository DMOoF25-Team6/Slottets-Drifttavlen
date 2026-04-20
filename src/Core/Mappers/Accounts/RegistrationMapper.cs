// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Account;

using Domain.Entities;

namespace Core.Mappers.Accounts;

/// <summary>
/// Maps registration DTOs to domain user entities.
/// </summary>
/// <remarks>
/// Used to transform <see cref="RegistrationRequestDto"/> into <see cref="User"/> for account creation workflows.
/// </remarks>
public static class RegistrationMapper
{
    /// <summary>
    /// Converts a <see cref="RegistrationRequestDto"/> to a <see cref="User"/> entity.
    /// </summary>
    /// <param name="request">A registration request containing user details.</param>
    /// <returns>A <see cref="User"/> entity populated from the registration request.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="request"/> is <see langword="null"/>.</exception>
    /// <example>
    /// <code language="csharp">
    /// var user = RegistrationMapper.ToUserEntity(new RegistrationRequestDto { EmailAddress = "user@example.com" });
    /// </code>
    /// </example>
    public static User ToUserEntity(RegistrationRequestDto request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return new User
        {
            Email = request.EmailAddress,
            UserName = request.EmailAddress // Assuming username is the same as email
        };
    }
}
