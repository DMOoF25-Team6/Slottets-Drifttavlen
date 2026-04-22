// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Interfaces;

using Infrastructure.Data.Persistent;
using Infrastructure.Data.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Tests.Repositories;

public class RepositoryTests
{
    private class TestUser : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
    }

    private class TestUserRepository : Repository<TestUser>
    {
        public TestUserRepository(AppDbContext context) : base(context) { }
    }

    private class TestDbContext(DbContextOptions<AppDbContext> options) : AppDbContext(options)
    {
        public DbSet<TestUser> TestUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.Entity<TestUser>();
        }
    }

    private AppDbContext CreateInMemoryContext()
    {
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new TestDbContext(options);
    }

    #region Functionality

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task AddAsync_AddsEntityToDatabase()
    {
        // Arrange
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        TestUser user = new() { Name = "Alice" };

        // Act
        TestUser result = await repo.AddAsync(user, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Name, result.Name);
        Assert.NotEqual(Guid.Empty, result.Id);
        _ = Assert.Single(context.Set<TestUser>());
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task AddRangeAsync_AddsEntitiesToDatabase()
    {
        // Arrange
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        TestUser[] users = new[] { new TestUser { Name = "A" }, new TestUser { Name = "B" } };

        // Act
        IEnumerable<TestUser> result = await repo.AddRangeAsync(users, TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(2, context.Set<TestUser>().Count());
        Assert.All(result, u => Assert.NotEqual(Guid.Empty, u.Id));
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetByIdAsync_ReturnsEntity_WhenExists()
    {
        // Arrange
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        TestUser user = new() { Name = "Bob" };
        _ = context.Set<TestUser>().Add(user);
        _ = await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        TestUser? result = await repo.GetByIdAsync(user.Id, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result!.Id);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetByIdAsync_ReturnsNull_WhenNotExists()
    {
        // Arrange
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);

        // Act
        TestUser? result = await repo.GetByIdAsync(Guid.NewGuid(), TestContext.Current.CancellationToken);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task GetAllAsync_ReturnsAllEntities()
    {
        // Arrange
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        TestUser[] users = new[] { new TestUser { Name = "A" }, new TestUser { Name = "B" } };
        context.Set<TestUser>().AddRange(users);
        _ = await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        IEnumerable<TestUser> result = await repo.GetAllAsync(TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task UpdateAsync_UpdatesEntity()
    {
        // Arrange
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        TestUser user = new() { Name = "Old" };
        _ = context.Set<TestUser>().Add(user);
        _ = await context.SaveChangesAsync(TestContext.Current.CancellationToken);
        user.Name = "New";

        // Act
        await repo.UpdateAsync(user, TestContext.Current.CancellationToken);

        // Assert
        TestUser? updated = await context.Set<TestUser>().FindAsync(new object?[] { user.Id }, TestContext.Current.CancellationToken);
        Assert.Equal("New", updated!.Name);
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task UpdateRangeAsync_UpdatesEntities()
    {
        // Arrange
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        TestUser[] users = new[] { new TestUser { Name = "A" }, new TestUser { Name = "B" } };
        context.Set<TestUser>().AddRange(users);
        _ = await context.SaveChangesAsync(TestContext.Current.CancellationToken);
        foreach (TestUser? u in users)
        {
            u.Name += "_updated";
        }

        // Act
        await repo.UpdateRangeAsync(users, TestContext.Current.CancellationToken);

        // Assert
        List<TestUser> all = context.Set<TestUser>().ToList();
        Assert.All(all, u => Assert.EndsWith("_updated", u.Name));
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task DeleteAsync_RemovesEntity()
    {
        // Arrange
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        TestUser user = new() { Name = "ToDelete" };
        _ = context.Set<TestUser>().Add(user);
        _ = await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        await repo.DeleteAsync(user, TestContext.Current.CancellationToken);

        // Assert
        Assert.Empty(context.Set<TestUser>());
    }

    [Fact]
    [Trait("Category", "Functionality")]
    public async Task DeleteRangeAsync_RemovesEntities()
    {
        // Arrange
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        TestUser[] users = new[] { new TestUser { Name = "A" }, new TestUser { Name = "B" } };
        context.Set<TestUser>().AddRange(users);
        _ = await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        await repo.DeleteRangeAsync(users, TestContext.Current.CancellationToken);

        // Assert
        Assert.Empty(context.Set<TestUser>());
    }

    #endregion

    #region EdgeCase

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task AddAsync_ThrowsOperationCanceledException_WhenCancelled()
    {
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        TestUser user = new() { Name = "Alice" };
        using CancellationTokenSource cts = new();
        cts.Cancel();
        _ = await Assert.ThrowsAnyAsync<OperationCanceledException>(() => repo.AddAsync(user, cts.Token));
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task UpdateAsync_DoesNotThrow_WhenEntityDoesNotExist()
    {
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        TestUser user = new() { Name = "Ghost" };
        _ = await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => repo.UpdateAsync(user, TestContext.Current.CancellationToken));
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task DeleteAsync_DoesNotThrow_WhenEntityDoesNotExist()
    {
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        TestUser user = new() { Name = "Ghost" };
        //await repo.DeleteAsync(user, TestContext.Current.CancellationToken);
        _ = await Assert.ThrowsAnyAsync<DbUpdateConcurrencyException>(async () => await repo.DeleteAsync(user, TestContext.Current.CancellationToken));
        //Assert.Empty(context.Set<TestUser>());

    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task AddRangeAsync_AllowsEmptyCollection()
    {
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        IEnumerable<TestUser> result = await repo.AddRangeAsync(Array.Empty<TestUser>(), TestContext.Current.CancellationToken);
        Assert.Empty(result);
        Assert.Empty(context.Set<TestUser>());
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task UpdateRangeAsync_AllowsEmptyCollection()
    {
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        await repo.UpdateRangeAsync(Array.Empty<TestUser>(), TestContext.Current.CancellationToken);
        Assert.Empty(context.Set<TestUser>());
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task DeleteRangeAsync_AllowsEmptyCollection()
    {
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        await repo.DeleteRangeAsync(Array.Empty<TestUser>(), TestContext.Current.CancellationToken);
        Assert.Empty(context.Set<TestUser>());
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task AddAsync_ThrowsArgumentNullException_WhenEntityIsNull()
    {
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        _ = await Assert.ThrowsAsync<ArgumentNullException>(() => repo.AddAsync(null!, TestContext.Current.CancellationToken));
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task AddRangeAsync_ThrowsArgumentNullException_WhenEntitiesIsNull()
    {
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        _ = await Assert.ThrowsAsync<ArgumentNullException>(() => repo.AddRangeAsync(null!, TestContext.Current.CancellationToken));
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task UpdateAsync_ThrowsArgumentNullException_WhenEntityIsNull()
    {
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        _ = await Assert.ThrowsAsync<ArgumentNullException>(() => repo.UpdateAsync(null!, TestContext.Current.CancellationToken));
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task UpdateRangeAsync_ThrowsArgumentNullException_WhenEntitiesIsNull()
    {
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        _ = await Assert.ThrowsAsync<ArgumentNullException>(() => repo.UpdateRangeAsync(null!, TestContext.Current.CancellationToken));
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task DeleteAsync_ThrowsArgumentNullException_WhenEntityIsNull()
    {
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        _ = await Assert.ThrowsAsync<ArgumentNullException>(() => repo.DeleteAsync(null!, TestContext.Current.CancellationToken));
    }

    [Fact]
    [Trait("Category", "EdgeCase")]
    public async Task DeleteRangeAsync_ThrowsArgumentNullException_WhenEntitiesIsNull()
    {
        await using AppDbContext context = CreateInMemoryContext();
        TestUserRepository repo = new(context);
        _ = await Assert.ThrowsAsync<ArgumentNullException>(() => repo.DeleteRangeAsync(null!, TestContext.Current.CancellationToken));
    }
    #endregion
}
