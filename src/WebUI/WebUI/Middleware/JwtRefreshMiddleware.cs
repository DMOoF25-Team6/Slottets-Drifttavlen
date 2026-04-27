// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//using Microsoft.Extensions.Primitives;
//using Microsoft.IdentityModel.Tokens;

//namespace WebUI.Middleware;

//public class JwtRefreshMiddleware
//{
//    private readonly RequestDelegate _next;
//    private readonly ILogger<JwtRefreshMiddleware> _logger;
//    private readonly IConfiguration _configuration;
//    private readonly int _refreshTimeoutSeconds;

//    public JwtRefreshMiddleware(RequestDelegate next, ILogger<JwtRefreshMiddleware> logger, IConfiguration configuration)
//    {
//        _next = next;
//        _logger = logger;
//        _configuration = configuration;
//        _refreshTimeoutSeconds = Math.Max(30, _configuration.GetValue<int>("JWT:RefreshTimeoutSeconds", 30));
//    }

//    public async Task InvokeAsync(HttpContext context)
//    {
//        // Check for refresh token in header
//        if (context.Request.Headers.TryGetValue("X-Refresh-Token", out StringValues refreshToken)
//            && !StringValues.IsNullOrEmpty(refreshToken))
//        {
//            // Validate refresh token (placeholder logic)
//            ClaimsPrincipal? principal = ValidateRefreshToken(refreshToken.ToString()!);
//            if (principal != null)
//            {
//                // Check if enough time has passed since last issue (optional, placeholder)
//                // Issue new JWT
//                string newJwt = GenerateJwtToken(principal);
//                context.Response.Headers["X-New-JWT"] = newJwt;
//            }
//        }
//        await _next(context);
//    }

//    private static ClaimsPrincipal? ValidateRefreshToken(string refreshToken)
//    {
//        // TODO: Implement real refresh token validation
//        // For now, accept any non-empty string as valid and return a dummy principal
//        if (!string.IsNullOrWhiteSpace(refreshToken))
//        {
//            Claim[] claims = [new Claim(ClaimTypes.Name, "user")];
//            ClaimsIdentity identity = new(claims, "refresh");
//            return new ClaimsPrincipal(identity);
//        }
//        return null;
//    }

//    private string GenerateJwtToken(ClaimsPrincipal principal)
//    {
//        // TODO: Use real JWT settings and claims
//        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["JWT:Key"] ?? "dev-secret-key"));
//        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
//        DateTime expires = DateTime.UtcNow.AddSeconds(_refreshTimeoutSeconds);
//        JwtSecurityToken token = new(
//            issuer: _configuration["JWT:Issuer"] ?? "dev-issuer",
//            audience: _configuration["JWT:Audience"] ?? "dev-audience",
//            claims: principal.Claims,
//            expires: expires,
//            signingCredentials: creds
//        );
//        return new JwtSecurityTokenHandler().WriteToken(token);
//    }
//}

//public static class JwtRefreshMiddlewareExtensions
//{
//    public static IApplicationBuilder UseJwtRefreshMiddleware(this IApplicationBuilder builder)
//    {
//        return builder.UseMiddleware<JwtRefreshMiddleware>();
//    }
//}
