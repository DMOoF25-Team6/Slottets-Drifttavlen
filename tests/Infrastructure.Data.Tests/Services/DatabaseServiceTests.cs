// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Infrastructure.Data.Persistent;
using Infrastructure.Data.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using Moq;

namespace Infrastructure.Data.Tests.Services;

/// <summary>
/// Unit tests for <see cref="DatabaseService"/>.
/// </summary>
public class DatabaseServiceTests
{
    // Test double for AppDbContext to allow injection of a mock DatabaseFacade
    private class TestDbContext(DbContextOptions<AppDbContext> options, DatabaseFacade databaseFacade)
        : AppDbContext(options)
    {
        public override DatabaseFacade Database => databaseFacade;
    }

    #region Functionality

    /// <summary>
    /// Verifies IsConnected returns true when the database can connect.
    /// </summary>
    [Fact]
    [Trait("Category", "Functionality")]
    [Trait("Service", nameof(DatabaseService))]
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

    /// <summary>
    /// Verifies IsConnected returns false when the database cannot connect.
    /// </summary>
    [Fact]
    [Trait("Category", "Functionality")]
    [Trait("Service", nameof(DatabaseService))]
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

    #endregion

    #region EdgeCase

    /// <summary>
    /// Verifies IsConnected returns false when an exception is thrown.
    /// </summary>
    [Fact]
    [Trait("Category", "EdgeCase")]
    [Trait("Service", nameof(DatabaseService))]
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

    #endregion
}
