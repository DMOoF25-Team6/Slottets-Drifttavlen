// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Core.DTOs.Identity;
using Core.Mappers.Accounts;

using Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers.Identity;

/// <summary>
/// Provides endpoints for user account registration, authentication, token refresh, and logout.
/// </summary>
/// <remarks>
/// This controller manages user identity operations such as registration, login, refresh token exchange, and logout.
/// </remarks>
[Route("[controller]")]
[ApiController]
public class AccountController(UserManager<User> userManager) : ControllerBase
{
    /// <summary>
    /// Registers a new user account.
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
    // In-memory refresh token store for demonstration. Replace with persistent store for production.
    private static readonly Dictionary<string, string> RefreshTokens = [];

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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        User? user = await userManager.FindByEmailAsync(request.Email);
        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized(new LoginResponseDto
            {
                ErrorMessages = ["Invalid email or password."]
            });
        }

        if (user.Email is null)
        {
            return Unauthorized(new LoginResponseDto
            {
                ErrorMessages = ["User email is not available."]
            });
        }

        string token = GenerateJwtToken();

        // Generate a secure refresh token
        string refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        // Store refresh token with user identifier (for demo, use email)
        lock (RefreshTokens)
        {
            RefreshTokens[user.Email] = refreshToken;
        }

        return Ok(new LoginResponseDto
        {
            JwtToken = token,
            RefreshToken = refreshToken
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
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(request.RefreshToken))
        {
            return BadRequest(new RefreshTokenResponseDto { ErrorMessages = ["Invalid refresh token request."] });
        }

        // Find user by refresh token
        string? userEmail = RefreshTokens.FirstOrDefault(x => x.Value == request.RefreshToken).Key;
        if (userEmail is null)
        {
            return Unauthorized(new RefreshTokenResponseDto { ErrorMessages = ["Invalid refresh token."] });
        }

        User? user = await userManager.FindByEmailAsync(userEmail);
        if (user is null)
        {
            return Unauthorized(new RefreshTokenResponseDto { ErrorMessages = ["User not found."] });
        }

        if (user.Email is null)
        {
            return Unauthorized(new RefreshTokenResponseDto { ErrorMessages = ["User email is not available."] });
        }

        string token = GenerateJwtToken();

        // Optionally, rotate refresh token
        string newRefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        lock (RefreshTokens)
        {
            RefreshTokens[user.Email] = newRefreshToken;
        }

        return Ok(new RefreshTokenResponseDto
        {
            JwtToken = token,
            RefreshToken = newRefreshToken
        });
    }

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the logout operation.</returns>
    /// <response code="200">Logout succeeded.</response>
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        // Here you would typically handle the logout logic, such as invalidating a JWT token or clearing a session
        // Clear the user's authentication cookie or token if applicable

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
        string key = Environment.GetEnvironmentVariable("Jwt__SecretKey") ?? throw new InvalidOperationException("IssuerSigningKey not found in environment variables.");
        SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(key));
        SigningCredentials signingCredentials = new(secretKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        JwtSecurityToken tokenOptions = new(
            issuer: Environment.GetEnvironmentVariable("Jwt__Issuer"),
            audience: Environment.GetEnvironmentVariable("Jwt__SecretKey"),
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
    #endregion
}
