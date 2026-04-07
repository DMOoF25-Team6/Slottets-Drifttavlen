// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Account;
using Core.Interfaces.Services;

namespace Core.Services;

public class AccountService : IAccountService
{
    public Task<RegistrationResponseDto> CreateAccountAsync(RegistrationRequestDto registrationRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<LogoutResponseDto> LogoutAsync(LogoutRequestDto logoutRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        throw new NotImplementedException();
    }
}
