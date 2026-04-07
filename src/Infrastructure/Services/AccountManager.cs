// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs.Account;
using Core.Interfaces.Managers;

namespace Infrastructure.Services;

/// <summary>
/// Provides account management operations by communicating with the backend API over HTTP.
/// </summary>
/// <remarks>
/// Implements <see cref="IAccountManager" /> for registration, login, logout, and token refresh.
/// </remarks>
public class AccountManager(HttpClient httpClient) : IAccountManager
{
    private readonly HttpClient _httpClient = httpClient;

    /// <summary>
    /// Creates a new user account by sending registration data to the backend API.
    /// </summary>
    /// <param name="registrationRequestDto">An object containing the registration details.</param>
    /// <returns>A response object containing the result of the registration operation.</returns>
    /// <remarks>
    /// Returns a failed response if the backend response cannot be parsed.
    /// </remarks>
    public async Task<RegistrationResponseDto> CreateAccountAsync(RegistrationRequestDto registrationRequestDto)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/accounts/register", registrationRequestDto);
        return response.Content.ReadFromJsonAsync<RegistrationResponseDto>().GetAwaiter().GetResult() is RegistrationResponseDto registrationResponseDto
            ? registrationResponseDto
            : new RegistrationResponseDto
            {
                IsSuccessful = false,
                ErrorMessages = ["Failed to parse registration response."]
            };
    }

    /// <summary>
    /// Authenticates a user by sending login credentials to the backend API.
    /// </summary>
    /// <param name="loginRequestDto">An object containing the login credentials.</param>
    /// <returns>A response object containing the result of the login operation.</returns>
    /// <remarks>
    /// Returns a failed response if the backend response cannot be parsed.
    /// </remarks>
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/accounts/login", loginRequestDto);
        return response.Content.ReadFromJsonAsync<LoginResponseDto>().GetAwaiter().GetResult() is LoginResponseDto loginResponseDto
            ? loginResponseDto
            : new LoginResponseDto
            {
                JwtToken = null,
                RefreshToken = null,
                ErrorMessages = ["Failed to parse login response."]
            };
    }

    /// <summary>
    /// Logs out a user by sending a logout request to the backend API.
    /// </summary>
    /// <param name="logoutRequestDto">An object containing the logout request details.</param>
    /// <returns>A response object containing the result of the logout operation.</returns>
    /// <remarks>
    /// Returns a failed response if the backend response cannot be parsed.
    /// </remarks>
    public async Task<LogoutResponseDto> LogoutAsync(LogoutRequestDto logoutRequestDto)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/accounts/logout", logoutRequestDto);
        return response.Content.ReadFromJsonAsync<LogoutResponseDto>().GetAwaiter().GetResult() is LogoutResponseDto logoutResponseDto
            ? logoutResponseDto
            : new LogoutResponseDto
            {
                IsSuccessful = false,
                ErrorMessages = ["Failed to parse logout response."]
            };
    }

    /// <summary>
    /// Refreshes the authentication token by sending a refresh token request to the backend API.
    /// </summary>
    /// <param name="refreshTokenRequestDto">An object containing the refresh token details.</param>
    /// <returns>A response object containing the result of the token refresh operation.</returns>
    /// <remarks>
    /// Returns a failed response if the backend response cannot be parsed.
    /// </remarks>
    public async Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/accounts/refresh-token", refreshTokenRequestDto);
        return response.Content.ReadFromJsonAsync<RefreshTokenResponseDto>().GetAwaiter().GetResult() is RefreshTokenResponseDto refreshTokenResponseDto
            ? refreshTokenResponseDto
            : new RefreshTokenResponseDto
            {
                JwtToken = null,
                RefreshToken = null,
                ErrorMessages = ["Failed to parse refresh token response."]
            };
    }
}
