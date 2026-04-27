//using System.Threading.Tasks;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Primitives;
//using Moq;
//using WebUI.Middleware;
//using Xunit;

//namespace WebUI.Tests.Middleware
//{
//    public class JwtRefreshMiddlewareInvokeAsyncTests
//    {
//        [Fact]
//        [Trait("Category", "Functionality")]
//        public async Task InvokeAsync_WithValidRefreshToken_SetsNewJwtHeader()
//        {
//            // Arrange
//            var context = new DefaultHttpContext();
//            context.Request.Headers["X-Refresh-Token"] = "valid-refresh-token";
//            var configMock = new Mock<IConfiguration>();
//            configMock.Setup(c => c["JWT:Key"]).Returns("test-secret-key-1234567890123456");
//            configMock.Setup(c => c["JWT:Issuer"]).Returns("test-issuer");
//            configMock.Setup(c => c["JWT:Audience"]).Returns("test-audience");
//            configMock.Setup(c => c.GetValue(It.IsAny<string>(), It.IsAny<int>())).Returns(60);
//            var loggerMock = new Mock<ILogger<JwtRefreshMiddleware>>();
//            var middleware = new JwtRefreshMiddleware(_ => Task.CompletedTask, loggerMock.Object, configMock.Object);

//            // Act
//            await middleware.InvokeAsync(context);

//            // Assert
//            Assert.True(context.Response.Headers.ContainsKey("X-New-JWT"));
//            Assert.False(string.IsNullOrWhiteSpace(context.Response.Headers["X-New-JWT"]));
//        }

//        [Fact]
//        [Trait("Category", "EdgeCase")]
//        public async Task InvokeAsync_WithoutRefreshToken_DoesNotSetJwtHeader()
//        {
//            // Arrange
//            var context = new DefaultHttpContext();
//            var configMock = new Mock<IConfiguration>();
//            configMock.Setup(c => c["JWT:Key"]).Returns("test-secret-key-1234567890123456");
//            configMock.Setup(c => c["JWT:Issuer"]).Returns("test-issuer");
//            configMock.Setup(c => c["JWT:Audience"]).Returns("test-audience");
//            configMock.Setup(c => c.GetValue(It.IsAny<string>(), It.IsAny<int>())).Returns(60);
//            var loggerMock = new Mock<ILogger<JwtRefreshMiddleware>>();
//            var middleware = new JwtRefreshMiddleware(_ => Task.CompletedTask, loggerMock.Object, configMock.Object);

//            // Act
//            await middleware.InvokeAsync(context);

//            // Assert
//            Assert.False(context.Response.Headers.ContainsKey("X-New-JWT"));
//        }
//    }
//}
