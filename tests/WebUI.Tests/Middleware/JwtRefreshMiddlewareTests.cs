//using System.Security.Claims;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Http;
//using Moq;
//using WebUI.Middleware;
//using Xunit;

//namespace WebUI.Tests.Middleware
//{
//    public class JwtRefreshMiddlewareTests
//    {
//        [Theory]
//        [InlineData("valid-refresh-token")]
//        [InlineData("another-token")]
//        [Trait("Category", "Functionality")]
//        public void ValidateRefreshToken_ValidToken_ReturnsPrincipal(string token)
//        {
//            // Act
//            var principal = JwtRefreshMiddleware.ValidateRefreshToken(token);

//            // Assert
//            Assert.NotNull(principal);
//            Assert.Equal("user", principal.Identity?.Name);
//            Assert.Equal("refresh", principal.Identity?.AuthenticationType);
//        }

//        [Theory]
//        [InlineData("")]
//        [InlineData(null)]
//        [Trait("Category", "EdgeCase")]
//        public void ValidateRefreshToken_InvalidToken_ReturnsNull(string token)
//        {
//            // Act
//            var principal = JwtRefreshMiddleware.ValidateRefreshToken(token);

//            // Assert
//            Assert.Null(principal);
//        }

//        [Fact]
//        [Trait("Category", "Functionality")]
//        public void GenerateJwtToken_ValidPrincipal_ReturnsToken()
//        {
//            // Arrange
//            var claims = new[] { new Claim(ClaimTypes.Name, "user") };
//            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "refresh"));
//            var configMock = new Mock<IConfiguration>();
//            configMock.Setup(c => c["JWT:Key"]).Returns("test-secret-key-1234567890123456");
//            configMock.Setup(c => c["JWT:Issuer"]).Returns("test-issuer");
//            configMock.Setup(c => c["JWT:Audience"]).Returns("test-audience");
//            configMock.Setup(c => c.GetValue(It.IsAny<string>(), It.IsAny<int>())).Returns(60);
//            var loggerMock = new Mock<ILogger<JwtRefreshMiddleware>>();
//            var middleware = new JwtRefreshMiddleware(_ => Task.CompletedTask, loggerMock.Object, configMock.Object);

//            // Act
//            var token = typeof(JwtRefreshMiddleware)
//                .GetMethod("GenerateJwtToken", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//                ?.Invoke(middleware, new object[] { principal }) as string;

//            // Assert
//            Assert.False(string.IsNullOrWhiteSpace(token));
//        }
//    }
//}
