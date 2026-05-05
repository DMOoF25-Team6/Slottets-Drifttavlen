// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Net.Http.Json;

using Core.DTOs;
using Core.DTOs.Identity;
using Core.Interfaces.Managers;
using Core.Mappers;

using Domain.Entities;

namespace Infrastructure.Managers;

/// <summary>
/// Provides operations for managing residents by communicating with the backend API over HTTP.
/// </summary>
/// <remarks>
/// Implements <see cref="IResidentManager"/> for retrieving and manipulating resident data.
/// </remarks>
public class ResidentManager : IResidentManager
{
    #region Fields
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory? _httpClientFactory;
    #endregion

    public ResidentManager(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient("SlottetApi") ?? throw new InvalidOperationException("Failed to create HttpClient.");
    }

    #region Methods create
    /// <summary>
    /// Creates a new user Account by sending registration data to the backend API.
    /// </summary>
    /// <param name="registrationRequestDto">An object containing the registration details.</param>
    /// <returns>A response object containing the result of the registration operation.</returns>
    /// <remarks>
    /// Returns a failed response if the backend response cannot be parsed.
    /// </remarks>
    public async Task<RegistrationResponseDto> CreateAccountAsync(RegisterRequestDto registrationRequestDto)
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

    public Task<IEnumerable<Resident>> CreateRangeAsync(IEnumerable<Resident> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    #endregion create


    ///// <summary>
    ///// Authenticates a user by sending login credentials to the backend API.
    ///// </summary>
    ///// <param name="loginRequestDto">An object containing the login credentials.</param>
    ///// <returns>A response object containing the result of the login operation.</returns>
    ///// <remarks>
    ///// Returns a failed response if the backend response cannot be parsed.
    ///// </remarks>
    //public async Task<ILoginResult> LoginAsync(LoginRequestDto loginRequestDto)
    //{
    //    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/Account/login", loginRequestDto);
    //    try
    //    {
    //        return await response.Content.ReadFromJsonAsync<LoginResponseDto>() is LoginResponseDto loginResponseDto
    //            ? loginResponseDto
    //            : new ErrorDto
    //            {
    //                ErrorMessages = ["Failed to parse login response."]
    //            };
    //    }
    //    catch (System.Text.Json.JsonException)
    //    {
    //        return new ErrorDto
    //        {
    //            ErrorMessages = ["Failed to parse login response."]
    //        };
    //    }
    //}

    ///// <summary>
    ///// Logs out a user by sending a logout request to the backend API.
    ///// </summary>
    ///// <param name="logoutRequestDto">An object containing the logout request details.</param>
    ///// <returns>A response object containing the result of the logout operation.</returns>
    ///// <remarks>
    ///// Returns a failed response if the backend response cannot be parsed.
    ///// </remarks>
    //public async Task<ILogoutResult> LogoutAsync(LogoutRequestDto logoutRequestDto)
    //{
    //    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/Account/logout", logoutRequestDto);
    //    try
    //    {
    //        ILogoutResult? logoutResponseDto = await response.Content.ReadFromJsonAsync<LogoutResponseDto>();
    //        return logoutResponseDto ?? new ErrorDto
    //        {
    //            ErrorMessages = ["Failed to parse logout response."]
    //        };
    //    }
    //    catch (System.Text.Json.JsonException)
    //    {
    //        return new ErrorDto
    //        {
    //            ErrorMessages = ["Failed to parse logout response."]
    //        };
    //    }
    //}

    ///// <summary>
    ///// Refreshes the authentication token by sending a refresh token request to the backend API.
    ///// </summary>
    ///// <param name="refreshTokenRequestDto">An object containing the refresh token details.</param>
    ///// <returns>A response object containing the result of the token refresh operation.</returns>
    ///// <remarks>
    ///// Returns a failed response if the backend response cannot be parsed.
    ///// </remarks>
    //public async Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto)
    //{
    //    HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/Account/refresh-token", refreshTokenRequestDto);
    //    try
    //    {
    //        RefreshTokenResponseDto? refreshTokenResponseDto = await response.Content.ReadFromJsonAsync<RefreshTokenResponseDto>();
    //        if (refreshTokenResponseDto != null)
    //        {
    //            return refreshTokenResponseDto;
    //        }
    //    }
    //    catch (System.Text.Json.JsonException)
    //    {
    //        // Fall through to return failed response
    //    }
    //    return new RefreshTokenResponseDto
    //    {
    //        JwtToken = null,
    //        RefreshToken = null,
    //        ErrorMessages = ["Failed to parse refresh token response."]
    //    };
    //}

    #region Methods read

    /// <summary>
    /// Gets a resident by their unique identifier.
    /// </summary>
    /// <param name="id">A unique identifier for the resident.</param>
    /// <param name="ct">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a resident if found; otherwise, <see langword="null"/>.
    /// </returns>
    public async Task<Resident?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        ResidentResponseDto? dto =
            await _httpClient.GetFromJsonAsync<ResidentResponseDto>(
                $"residents/{id}", ct);
        return dto != null ? ResidentMapper.ToResident(dto) : null;
    }

    /// <summary>
    /// Gets all residents.
    /// </summary>
    /// <param name="ct">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a collection of residents.
    /// </returns>
    public async Task<IEnumerable<Resident>> GetAllAsync(CancellationToken ct = default)
    {
        IEnumerable<ResidentResponseDto>? dtos =
            await _httpClient.GetFromJsonAsync<IEnumerable<ResidentResponseDto>>(
                "residents", ct);
        return dtos != null ? dtos.Select(ResidentMapper.ToResident) : [];
    }

    /// <summary>
    /// Adds a new resident.
    /// </summary>
    /// <param name="entity">A resident entity to add.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the added resident.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Always thrown as this method is not implemented.
    /// </exception>
    public Task<Resident> CreateAsync(Resident entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Adds a range of residents.
    /// </summary>
    /// <param name="entities">A collection of resident entities to add.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the added residents.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Always thrown as this method is not implemented.
    /// </exception>
    public Task<IEnumerable<Resident>> AddRangeAsync(IEnumerable<Resident> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates a resident.
    /// </summary>
    /// <param name="entity">A resident entity to update.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Always thrown as this method is not implemented.
    /// </exception>
    public Task UpdateAsync(Resident entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates a range of residents.
    /// </summary>
    /// <param name="entities">A collection of resident entities to update.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Always thrown as this method is not implemented.
    /// </exception>
    public Task UpdateRangeAsync(IEnumerable<Resident> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deletes a resident.
    /// </summary>
    /// <param name="entity">A resident entity to delete.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Always thrown as this method is not implemented.
    /// </exception>
    public Task DeleteAsync(Resident entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deletes a range of residents.
    /// </summary>
    /// <param name="entities">A collection of resident entities to delete.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Always thrown as this method is not implemented.
    /// </exception>
    public Task DeleteRangeAsync(IEnumerable<Resident> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }


    #endregion
}
