// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs.Account;
using Core.Interfaces.Managers;
using Core.Interfaces.Services;

namespace Core.Services;

public class AccountService(IAccountManager accountManager) : IAccountService
{
    private readonly IAccountManager _accountManager = accountManager;

    public Task<RegistrationResponseDto> CreateAccountAsync(RegistrationRequestDto registrationRequestDto)
    {
        return _accountManager.CreateAccountAsync(registrationRequestDto);
    }

    public Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        return _accountManager.LoginAsync(loginRequestDto);
    }

    public Task<LogoutResponseDto> LogoutAsync(LogoutRequestDto logoutRequestDto)
    {
        return _accountManager.LogoutAsync(logoutRequestDto);
    }

    public Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        return _accountManager.RefreshTokenAsync(refreshTokenRequestDto);
    }
}
