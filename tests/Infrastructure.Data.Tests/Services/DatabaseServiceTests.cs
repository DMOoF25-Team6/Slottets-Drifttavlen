// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Infrastructure.Data.Persistent;
using Infrastructure.Data.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using Moq;

namespace Infrastructure.Data.Tests.Services;

public class DatabaseServiceTests
{
    // Test double for AppDbContext to allow injection of a mock DatabaseFacade
    private class TestDbContext(DbContextOptions<AppDbContext> options, DatabaseFacade databaseFacade) : AppDbContext(options)
    {
        public override DatabaseFacade Database => databaseFacade;
    }

    [Fact]
    public void IsConnected_ReturnsTrue_WhenDatabaseCanConnect()
    {
        // Arrange
        Mock<DatabaseFacade> dbFacadeMock = new(MockBehavior.Strict, [new Mock<AppDbContext>(new DbContextOptions<AppDbContext>()).Object]);
        _ = dbFacadeMock.Setup(x => x.CanConnect()).Returns(true);
        TestDbContext dbContext = new(new DbContextOptions<AppDbContext>(), dbFacadeMock.Object);
        DatabaseService service = new(dbContext);

        // Act
        bool result = service.IsConnected();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsConnected_ReturnsFalse_WhenDatabaseCannotConnect()
    {
        // Arrange
        Mock<DatabaseFacade> dbFacadeMock = new(MockBehavior.Strict, [new Mock<AppDbContext>(new DbContextOptions<AppDbContext>()).Object]);
        _ = dbFacadeMock.Setup(x => x.CanConnect()).Returns(false);
        TestDbContext dbContext = new(new DbContextOptions<AppDbContext>(), dbFacadeMock.Object);
        DatabaseService service = new(dbContext);

        // Act
        bool result = service.IsConnected();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsConnected_ReturnsFalse_WhenExceptionThrown()
    {
        // Arrange
        Mock<DatabaseFacade> dbFacadeMock = new(MockBehavior.Strict, [new Mock<AppDbContext>(new DbContextOptions<AppDbContext>()).Object]);
        _ = dbFacadeMock.Setup(x => x.CanConnect()).Throws(new Exception("Connection error"));
        TestDbContext dbContext = new(new DbContextOptions<AppDbContext>(), dbFacadeMock.Object);
        DatabaseService service = new(dbContext);

        // Act
        bool result = service.IsConnected();

        // Assert
        Assert.False(result);
    }
}
