// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Core.Interfaces.Services;

using Domain.Entities;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services;

/// <summary>
/// Service for generating JWT tokens for users.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TokenService"/> class.
/// </remarks>
/// <param name="configuration">The application configuration.</param>
/// <param name="logger">The logger instance.</param>
public class TokenService(IConfiguration configuration, ILogger<TokenService> logger) : ITokenService
{

    /// <summary>
    /// Generates a JWT token for the specified user and roles.
    /// </summary>
    /// <param name="user">The user entity.</param>
    /// <param name="roles">The list of roles for the user.</param>
    /// <returns>A JWT token string.</returns>
    public string GenerateToken(User user, IList<string> roles)
    {
        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.UserName!),
            new (ClaimTypes.Email, user.Email!),
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            .. roles.Select(role => new Claim(ClaimTypes.Role, role)),
        ];

        // Log the roles for debugging
        logger.LogInformation("Generating token for user {UserId} with roles: {Roles}", user.Id, string.Join(",", roles));

        SymmetricSecurityKey signingKey = new(Encoding.UTF8.GetBytes(configuration["TokenValidationParameters:IssuerSigningKey"]!));
        SigningCredentials credentials = new(signingKey, SecurityAlgorithms.HmacSha256);
        int minutes = int.Parse(configuration["TokenValidationParameters:ExpireMinutes"] ?? "60");

        JwtSecurityToken token = new(
            issuer: configuration["TokenValidationParameters:Issuer"],
            audience: configuration["TokenValidationParameters:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(minutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
