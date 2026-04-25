// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs.Identity;
using Core.Interfaces.Managers;

namespace Infrastructure.Managers;

/// <summary>
/// Provides Account management operations by communicating with the backend API over HTTP.
/// </summary>
/// <remarks>
/// Implements <see cref="IAccountManager" /> for registration, login, logout, and token refresh.
/// </remarks>
public class AccountManager : IAccountManager
{
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory? _httpClientFactory;

    public AccountManager(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient("SlottetApi") ?? throw new InvalidOperationException("Failed to create HttpClient.");
    }

    /// <summary>
    /// Creates a new user Account by sending registration data to the backend API.
    /// </summary>
    /// <param name="registrationRequestDto">An object containing the registration details.</param>
    /// <returns>A response object containing the result of the registration operation.</returns>
    /// <remarks>
    /// Returns a failed response if the backend response cannot be parsed.
    /// </remarks>
    public async Task<RegistrationResponseDto> Register(RegisterRequestDto registrationRequestDto)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/Account/register", registrationRequestDto);
        try
        {
            return response.Content.ReadFromJsonAsync<RegistrationResponseDto>().GetAwaiter().GetResult() is RegistrationResponseDto registrationResponseDto
                ? registrationResponseDto
                : new RegistrationResponseDto
                {
                    IsSuccessful = false,
                    ErrorMessages = ["Failed to parse registration response."]
                };
        }
        catch (System.Text.Json.JsonException)
        {
            return new RegistrationResponseDto
            {
                IsSuccessful = false,
                ErrorMessages = ["Failed to parse registration response."]
            };
        }
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
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/Account/login", loginRequestDto);
        try
        {
            return await response.Content.ReadFromJsonAsync<LoginResponseDto>() is LoginResponseDto loginResponseDto
                ? loginResponseDto
                : new LoginResponseDto
                {
                    JwtToken = null,
                    RefreshToken = null,
                    ErrorMessages = ["Failed to parse login response."]
                };
        }
        catch (System.Text.Json.JsonException)
        {
            return new LoginResponseDto
            {
                JwtToken = null,
                RefreshToken = null,
                ErrorMessages = ["Failed to parse login response."]
            };
        }
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
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/Account/logout", logoutRequestDto);
        try
        {
            LogoutResponseDto? logoutResponseDto = await response.Content.ReadFromJsonAsync<LogoutResponseDto>();
            return logoutResponseDto ?? new LogoutResponseDto
            {
                IsSuccessful = false,
                ErrorMessages = ["Failed to parse logout response."]
            };
        }
        catch (System.Text.Json.JsonException)
        {
            return new LogoutResponseDto
            {
                IsSuccessful = false,
                ErrorMessages = ["Failed to parse logout response."]
            };
        }
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
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/Account/refresh-token", refreshTokenRequestDto);
        try
        {
            RefreshTokenResponseDto? refreshTokenResponseDto = await response.Content.ReadFromJsonAsync<RefreshTokenResponseDto>();
            if (refreshTokenResponseDto != null)
            {
                return refreshTokenResponseDto;
            }
        }
        catch (System.Text.Json.JsonException)
        {
            // Fall through to return failed response
        }
        return new RefreshTokenResponseDto
        {
            JwtToken = null,
            RefreshToken = null,
            ErrorMessages = ["Failed to parse refresh token response."]
        };
    }

    public Task<RegistrationResponseDto> CreateAccountAsync(RegisterRequestDto registrationRequestDto)
    {
        throw new NotImplementedException();
    }
}
