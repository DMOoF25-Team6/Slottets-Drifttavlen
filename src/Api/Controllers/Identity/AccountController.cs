// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Core.DTOs.Identity;
using Core.Interfaces.Services;
using Core.Mappers.Accounts;

using Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers.Identity;

/// <summary>
/// Provides endpoints for user Account registration, authentication, token refresh, and logout.
/// </summary>
/// <remarks>
/// This controller manages user identity operations such as registration, login, refresh token exchange, and logout.
/// </remarks>
[Route("[controller]")]
[ApiController]
public class AccountController(UserManager<User> userManager, IRefreshTokenStore refreshTokenStore) : ControllerBase
{
    /// <summary>
    /// Registers a new user Account.
    /// </summary>
    /// <param name="request">A registration request containing user details and password.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the registration operation.</returns>
    /// <response code="200">Registration succeeded.</response>
    /// <response code="400">Registration failed due to validation errors or duplicate user.</response>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        User user = RegistrationMapper.ToUserEntity(request);
        IdentityResult result = await userManager.CreateAsync(user, request.Password);
        return result.Succeeded
            ? Ok(new RegistrationResponseDto { IsSuccessful = true })
            : BadRequest(new RegistrationResponseDto
            {
                IsSuccessful = false,
                ErrorMessages = result.Errors.Select(e => e.Description)
            });
    }


    /// <summary>
    /// Authenticates a user and returns a JWT access token and refresh token.
    /// </summary>
    /// <param name="request">A login request containing user credentials.</param>
    /// <returns>An <see cref="IActionResult"/> containing the JWT and refresh token if authentication is successful.</returns>
    /// <response code="200">Authentication succeeded.</response>
    /// <response code="401">Authentication failed due to invalid credentials.</response>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        if (!IsValidRequest(ModelState, out IActionResult? errorResult))
        {
            return errorResult;
        }

        User? user = await FindValidUserAsync(request.Email, request.Password);
        if (user == null)
        {
            return Unauthorized(new LoginResponseDto { ErrorMessages = ["Invalid email or password."] });
        }

        string token = GenerateJwtToken();
        string refreshTokenValue = GenerateRefreshToken();
        string? ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        RefreshToken refreshToken = new()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshTokenValue,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedByIp = ipAddress
        };

        await refreshTokenStore.SaveAsync(refreshToken);

        return Ok(new LoginResponseDto
        {
            JwtToken = token,
            RefreshToken = refreshTokenValue
        });
    }

    /// <summary>
    /// Exchanges a valid refresh token for a new JWT access token.
    /// </summary>
    /// <param name="request">A refresh token request containing the refresh token to exchange.</param>
    /// <returns>An <see cref="IActionResult"/> containing a new JWT and refresh token if the request is valid.</returns>
    /// <response code="200">Refresh succeeded and new tokens are returned.</response>
    /// <response code="401">Refresh failed due to invalid or expired refresh token.</response>
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
    {
        IActionResult? validationError = ValidateRefreshRequest(request);
        if (validationError != null)
        {
            return validationError;
        }

        RefreshToken? refreshToken = await GetValidRefreshTokenAsync(request.RefreshToken);
        if (refreshToken == null)
        {
            return Unauthorized(new RefreshTokenResponseDto { ErrorMessages = ["Invalid or expired refresh token."] });
        }

        // Revoke the old refresh token
        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.RevokedReason = "Rotated by refresh endpoint";
        await refreshTokenStore.SaveAsync(refreshToken);

        User? user = await GetValidUserByIdAsync(refreshToken.UserId);
        if (user == null)
        {
            return Unauthorized(new RefreshTokenResponseDto { ErrorMessages = ["User not found or email unavailable."] });
        }

        string token = GenerateJwtToken();
        string? ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        RefreshToken newRefreshToken = new()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = GenerateRefreshToken(),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedByIp = ipAddress
        };
        await refreshTokenStore.SaveAsync(newRefreshToken);

        return Ok(new RefreshTokenResponseDto
        {
            JwtToken = token,
            RefreshToken = newRefreshToken.Token
        });
    }

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the logout operation.</returns>
    /// <response code="200">Logout succeeded.</response>
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync([FromBody] LogoutRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Expect refresh token in request header or body for revocation
        string? refreshToken = Request.Headers["X-Refresh-Token"].FirstOrDefault() ?? request.RefreshToken;
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return BadRequest(new LogoutResponseDto
            {
                IsSuccessful = false,
                ErrorMessages = ["Refresh token required for logout."]
            });
        }

        // Revoke the refresh token
        RefreshToken? tokenEntity = await refreshTokenStore.GetByTokenAsync(refreshToken);
        if (tokenEntity is not null)
        {
            tokenEntity.RevokedAt = DateTime.UtcNow;
            tokenEntity.RevokedReason = "User logout";
            await refreshTokenStore.SaveAsync(tokenEntity);
        }

        return Ok(new LogoutResponseDto
        {
            IsSuccessful = true
        });
    }

    #region Helper Methods
    /// <summary>
    /// Generates a new JWT access token.
    /// </summary>
    /// <remarks>
    /// Uses environment variables for signing key, issuer, and audience. Throws if signing key is not found.
    /// </remarks>
    /// <returns>A JWT access token as a string.</returns>
    /// <exception cref="InvalidOperationException">IssuerSigningKey not found in environment variables.</exception>
    private static string GenerateJwtToken()
    {
        string key = Environment.GetEnvironmentVariable("TokenValidationParameters__IssuerSigningKey") ?? throw new InvalidOperationException("IssuerSigningKey not found in environment variables.");
        SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(key));
        SigningCredentials signingCredentials = new(secretKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        JwtSecurityToken tokenOptions = new(
            issuer: Environment.GetEnvironmentVariable("TokenValidationParameters__Issuer"),
            audience: Environment.GetEnvironmentVariable("TokenValidationParameters__Audience"),
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    // Helper: Validate ModelState
    private static bool IsValidRequest(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState, out IActionResult errorResult)
    {
        if (!modelState.IsValid)
        {
            errorResult = new BadRequestObjectResult(modelState);
            return false;
        }
        errorResult = null!;
        return true;
    }

    // Helper: Validate Refresh request and return error if invalid
    private BadRequestObjectResult? ValidateRefreshRequest(RefreshTokenRequestDto request)
    {
        return !ModelState.IsValid || string.IsNullOrWhiteSpace(request.RefreshToken)
            ? BadRequest(new RefreshTokenResponseDto { ErrorMessages = ["Invalid refresh token request."] })
            : null;
    }

    // Helper: Retrieve and validate refresh token
    private async Task<RefreshToken?> GetValidRefreshTokenAsync(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }

        RefreshToken? refreshToken = await refreshTokenStore.GetByTokenAsync(token);
        return refreshToken == null || refreshToken.ExpiresAt < DateTime.UtcNow || refreshToken.RevokedAt != null ? null : refreshToken;
    }

    // Helper: Retrieve and validate user by id
    private async Task<User?> GetValidUserByIdAsync(Guid userId)
    {
        User? user = await userManager.FindByIdAsync(userId.ToString());
        return user == null || string.IsNullOrWhiteSpace(user.Email) ? null : user;
    }

    // Helper: Rotate and save refresh token
    private async Task<RefreshToken> RotateAndSaveRefreshTokenAsync(User user)
    {
        string newRefreshTokenValue = GenerateRefreshToken();
        RefreshToken newRefreshToken = new()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = newRefreshTokenValue,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };
        await refreshTokenStore.SaveAsync(newRefreshToken);
        return newRefreshToken;
    }

    // Helper: Generate refresh token
    private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

    // Helper: Find user and check password
    private async Task<User?> FindValidUserAsync(string email, string password)
    {
        User? user = await userManager.FindByEmailAsync(email);
        return user == null || !await userManager.CheckPasswordAsync(user, password) || string.IsNullOrWhiteSpace(user.Email) ? null : user;
    }

    #endregion
}
