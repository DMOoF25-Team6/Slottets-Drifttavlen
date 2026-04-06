// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Accounts;

using Domain.Entities;

namespace Core.Mappers.Accounts;

public static class RegistrationMapper
{
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
