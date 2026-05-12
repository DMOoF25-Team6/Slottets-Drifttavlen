// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Core.Interfaces.Services;
using Core.Services;

using Domain.Entities;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Moq;

namespace Core.Tests.Services;

public class TokenServiceTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<ILogger<TokenService>> _loggerMock;
    private readonly Mock<IRefreshTokenStore> _refreshTokenStoreMock;
    private readonly TokenService _tokenService;

    public TokenServiceTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _loggerMock = new Mock<ILogger<TokenService>>();
        _refreshTokenStoreMock = new Mock<IRefreshTokenStore>();
        // Setup default config values
        _ = _configurationMock.Setup(c => c["TokenValidationParameters:IssuerSigningKey"]).Returns("supersecretkey12345678901234567890");
        _ = _configurationMock.Setup(c => c["TokenValidationParameters:Issuer"]).Returns("TestIssuer");
        _ = _configurationMock.Setup(c => c["TokenValidationParameters:Audience"]).Returns("TestAudience");
        _ = _configurationMock.Setup(c => c["TokenValidationParameters:ExpireMinutes"]).Returns("60");
        _ = _configurationMock.Setup(c => c["TokenValidationParameters:TokenExpirationMinutes"]).Returns("120");
        _tokenService = new TokenService(_configurationMock.Object, _loggerMock.Object, _refreshTokenStoreMock.Object, _configurationMock.Object);
    }

    #region Functionality
    [Fact]
    [Trait("Category", "Functionality")]
    public async Task CreateJwtTokenAsync_ValidUserAndRoles_ReturnsToken()
    {
        // Arrange
        User user = new() { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@example.com" };
        List<string> roles = ["Admin", "User"];
        List<Claim> permissions = [new Claim("Permission", "Read"), new Claim("Permission", "Write")];

        // Act
        string token = await _tokenService.CreateJwtTokenAsync(user, roles, permissions);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(token));
        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken jwt = handler.ReadJwtToken(token);
        Assert.Equal(user.Id.ToString(), jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        Assert.Contains(jwt.Claims, c => c.Type == ClaimTypes.Role && c.Value == "Admin");
        Assert.Contains(jwt.Claims, c => c.Type == "Permission" && c.Value == "Read");
    }
    #endregion

    #region EdgeCase
    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task CreateJwtTokenAsync_NullUser_ThrowsArgumentNullException()
    {
        // Arrange
        User? user = null;
        List<string> roles = [];
        List<Claim> permissions = [];

        // Act & Assert
        _ = await Assert.ThrowsAsync<ArgumentNullException>(() => _tokenService.CreateJwtTokenAsync(user!, roles, permissions));
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task CreateJwtTokenAsync_EmptyRolesAndPermissions_StillReturnsToken()
    {
        // Arrange
        User user = new() { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@example.com" };
        List<string> roles = [];
        List<Claim> permissions = [];

        // Act
        string token = await _tokenService.CreateJwtTokenAsync(user, roles, permissions);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(token));
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task CreateJwtTokenAsync_MissingConfig_ThrowsException()
    {
        // Arrange
        User user = new() { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@example.com" };
        List<string> roles = [];
        List<Claim> permissions = [];
        _ = _configurationMock.Setup(c => c["TokenValidationParameters:IssuerSigningKey"]).Returns((string?)null);

        // Act & Assert
        _ = await Assert.ThrowsAsync<ArgumentNullException>(() => _tokenService.CreateJwtTokenAsync(user, roles, permissions));
    }
    #endregion

    #region Concurrency
    [Fact]
    [Trait("Category", "Concurrency")]
    public async Task CreateJwtTokenAsync_ConcurrentCalls_ReturnsUniqueTokens()
    {
        // Arrange
        User user = new() { Id = Guid.NewGuid(), UserName = "testuser", Email = "test@example.com" };
        List<string> roles = ["User"];
        List<Claim> permissions = [];
        IEnumerable<Task<string>> tasks = Enumerable.Range(0, 10).Select(_ => _tokenService.CreateJwtTokenAsync(user, roles, permissions));

        // Act
        string[] tokens = await Task.WhenAll(tasks);

        // Assert
        Assert.Equal(10, tokens.Distinct().Count());
    }
    #endregion

    #region Functionality
    [Fact]
    [Trait("Category", "Functionality")]
    public async Task CreateRefreshTokenAsync_ValidUser_ReturnsRefreshTokenAndPersists()
    {
        // Arrange
        User user = new() { Id = Guid.NewGuid() };
        string ip = "127.0.0.1";
        _refreshTokenStoreMock.Setup(s => s.SaveAsync(It.IsAny<RefreshToken>(), It.IsAny<ITokenService>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();

        // Act
        RefreshToken refreshToken = await _tokenService.CreateRefreshTokenAsync(user, ip);

        // Assert
        Assert.NotNull(refreshToken);
        Assert.Equal(user.Id, refreshToken.UserId);
        Assert.Equal(ip, refreshToken.CreatedByIp);
        _refreshTokenStoreMock.Verify(s => s.SaveAsync(It.IsAny<RefreshToken>(), It.IsAny<ITokenService>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    #endregion

    #region EdgeCase
    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task CreateRefreshTokenAsync_NullUser_ThrowsArgumentNullException()
    {
        // Arrange
        User? user = null;
        string ip = "127.0.0.1";

        // Act & Assert
        _ = await Assert.ThrowsAsync<ArgumentNullException>(() => _tokenService.CreateRefreshTokenAsync(user!, ip));
    }
    #endregion

    #region Functionality
    [Fact]
    [Trait("Category", "Functionality")]
    public async Task ComputeSha256Hash_ValidInput_ReturnsHash()
    {
        // Arrange
        string input = "teststring";

        // Act
        string hash = await _tokenService.ComputeSha256Hash(input);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(hash));
        Assert.Equal(64, hash.Length); // SHA256 hash length in hex
    }
    #endregion

    #region EdgeCase
    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task ComputeSha256Hash_EmptyString_ReturnsHash()
    {
        // Arrange
        string input = string.Empty;

        // Act
        string hash = await _tokenService.ComputeSha256Hash(input);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(hash));
        Assert.Equal(64, hash.Length);
    }
    #endregion
}
