// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
public class TokenService(
    IConfiguration configuration,
    ILogger<TokenService> logger) : ITokenService
{
    private readonly IConfiguration configuration = configuration;
    private readonly ILogger<TokenService> logger = logger;

    /// <summary>
    /// Generates a JWT token for the specified user and roles.
    /// </summary>
    /// <param name="user">The user entity.</param>
    /// <param name="roles">The list of roles for the user.</param>
    /// <param name="permission">The list of permissions for the user.</param>
    /// <returns>A JWT token string.</returns>
    /// <summary>
    /// Generates a JWT token for the specified user and roles.
    /// </summary>
    /// <param name="user">The user entity.</param>
    /// <param name="roles">The list of roles for the user.</param>
    /// <param name="permissions">The list of permissions for the user.</param>
    /// <returns>A JWT token string.</returns>
    public async Task<string> CreateJwtTokenAsync(User user, IList<string> roles, IList<System.Security.Claims.Claim> permissions, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);
        List<Claim> claims =
        [
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            ..roles.Select(role => new Claim(ClaimTypes.Role, role)),
            ..permissions.Select(p => new Claim("Permission", p.Value))
        ];

        if (user.Department.HasValue)
        {
            claims.Add(new Claim("Department", user.Department.Value.ToString()));
        }

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

        return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }

    /// <summary>
    /// Generates a JWT token for the specified user and roles, supporting cancellation token.
    /// </summary>
    /// <param name="user">The user entity.</param>
    /// <param name="roles">The list of roles for the user.</param>
    /// <param name="permissions">The list of permissions for the user.</param>
    /// <param name="cancellationToken">A cancellation token (currently not used).</param>
    /// <returns>A JWT token string.</returns>
    public async Task<RefreshToken> CreateRefreshTokenAsync(User user, string ipAddress, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);
        RefreshToken refreshToken = new()
        {
            UserId = user.Id,
            TokenHash = await ComputeRefreshTokenAsync(),
            ExpiresAt = DateTime.UtcNow.AddMinutes(int.Parse(configuration["TokenValidationParameters:TokenExpirationMinutes"] ?? "60")),
            CreatedByIp = ipAddress,
            CreatedAt = DateTime.UtcNow
        };
        return refreshToken;
    }

    /// <summary>
    /// Computes the SHA-256 hash of a given string and returns it as a hex string.
    /// </summary>
    /// <param name="token">The input string to hash.</param>
    /// <returns>Hex-encoded SHA-256 hash.</returns>
    public async Task<string> ComputeSha256HashAsync(string token, CancellationToken cancellationToken = default)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(token);
        byte[] hash = SHA256.HashData(bytes);
        return await Task.FromResult(string.Concat(hash.Select(b => b.ToString("x2"))));
    }

    #region Helper Methods
    /// <summary>
    /// Generates a cryptographically secure refresh token as a Base64-encoded string.
    /// </summary>
    /// <remarks>The generated token is suitable for use in authentication scenarios where a secure, random
    /// value is required. Each call produces a unique token.</remarks>
    /// <returns>A Base64-encoded string representing a newly generated refresh token.</returns>
    private async Task<string> ComputeRefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        byte[] randomBytes = new byte[32];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }
        return await ComputeSha256HashAsync(Convert.ToBase64String(randomBytes), cancellationToken);
    }

    private Task<bool> IsValidAsync(string token)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new();
            _ = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["TokenValidationParameters:Issuer"],
                ValidAudience = configuration["TokenValidationParameters:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenValidationParameters:IssuerSigningKey"]!))
            }, out SecurityToken validatedToken);
            return Task.FromResult(validatedToken != null);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Invalid token validation attempt.");
            return Task.FromResult(false);
        }
    }

    #endregion
}
