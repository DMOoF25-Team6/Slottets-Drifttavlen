// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Core.DTOs.Accounts;
using Core.Mappers.Accounts;

using Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers.Accounts;

[Route("[controller]")]
[ApiController]
public class AccountController(UserManager<User> userManager) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDto request)
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
                Errors = result.Errors.Select(e => e.Description)
            });
    }

    // In-memory refresh token store for demonstration. Replace with persistent store for production.
    private static readonly Dictionary<string, string> RefreshTokens = [];

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        User? user = await userManager.FindByEmailAsync(request.EmailAddress);
        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized(new LoginResponseDto
            {
                ErrorMessage = "Invalid email or password."
            });
        }

        if (user.Email is null)
        {
            return Unauthorized(new LoginResponseDto
            {
                ErrorMessage = "User email is not available."
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
    /// Exchange a valid refresh token for a new JWT access token.
    /// </summary>
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(request.RefreshToken))
        {
            return BadRequest(new RefreshTokenResponseDto { ErrorMessage = "Invalid refresh token request." });
        }

        // Find user by refresh token
        string? userEmail = RefreshTokens.FirstOrDefault(x => x.Value == request.RefreshToken).Key;
        if (userEmail is null)
        {
            return Unauthorized(new RefreshTokenResponseDto { ErrorMessage = "Invalid refresh token." });
        }

        User? user = await userManager.FindByEmailAsync(userEmail);
        if (user is null)
        {
            return Unauthorized(new RefreshTokenResponseDto { ErrorMessage = "User not found." });
        }

        if (user.Email is null)
        {
            return Unauthorized(new RefreshTokenResponseDto { ErrorMessage = "User email is not available." });
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

    #region Helpter Methods
    private static string GenerateJwtToken()
    {
        string key = Environment.GetEnvironmentVariable("TokenValidationParameters__IssuerSigningKey") ?? throw new InvalidOperationException("IssuerSigningKey not found in environment variables.");
        SymmetricSecurityKey secretKey = new(Encoding.UTF8.GetBytes(key));
        SigningCredentials signingCredentials = new(secretKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims = new[]
        {
            new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        JwtSecurityToken tokenOptions = new(
            issuer: Environment.GetEnvironmentVariable("TokenValidationParameters__ValidIssuer"),
            audience: Environment.GetEnvironmentVariable("TokenValidationParameters__ValidAudience"),
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
    #endregion
}
