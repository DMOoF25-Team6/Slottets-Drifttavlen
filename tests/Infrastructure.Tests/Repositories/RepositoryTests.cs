// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Domain.Interfaces;

using Infrastructure.Repositories;

namespace Infrastructure.Tests.Repositories;

public class User : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
public class UserRepository : Repository<User>
{
    // No extra implementation needed for in-memory tests
}

/// <summary>
/// Unit tests for the <see cref="Repository{TEntity}"/> class.
/// </summary>
public class RepositoryTests
{
    private readonly UserRepository _repository;

    public RepositoryTests()
    {
        _repository = new UserRepository();
    }

    [Theory]
    [InlineData("Bob")]
    [InlineData("Charlie")]
    public async Task AddAsync_MultipleUsers_UsersAreAdded(string name)
    {
        // Arrange
        User user = new() { Name = name };

        // Act
        User result = await _repository.AddAsync(user, TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(name, result.Name);
        Assert.Contains(result, _repository.Entities);
    }

    // AddRangeAsync
    [Theory]
    [InlineData("Alice", "Bob")]
    [InlineData("Charlie", "Dave")]
    public async Task AddRangeAsync_MultipleUsers_UsersAreAdded(string name1, string name2)
    {
        // Arrange
        List<User> users =
        [
            new User { Name = name1 },
            new User { Name = name2 }
        ];
        // Act
        IEnumerable<User> results = await _repository.AddRangeAsync(users, TestContext.Current.CancellationToken);
        // Assert
        Assert.Contains(results, u => u.Name == name1);
        Assert.Contains(results, u => u.Name == name2);
        Assert.All(results, r => Assert.Contains(r, _repository.Entities));
    }

    [Fact]
    public async Task DeleteAsync_User_UserIsRemoved()
    {
        // Arrange
        User user = new() { Name = "Dave" };
        _ = await _repository.AddAsync(user, TestContext.Current.CancellationToken);

        // Act
        await _repository.DeleteAsync(user, TestContext.Current.CancellationToken);

        // Assert
        Assert.DoesNotContain(user, _repository.Entities);
    }

    //DeleteRangeAsync
    [Theory]
    [InlineData("Eve", "Frank")]
    [InlineData("Grace", "Heidi")]
    public async Task DeleteRangeAsync_MultipleUsers_UsersAreRemoved(string name1, string name2)
    {
        // Arrange
        List<User> users =
        [
            new User { Name = name1 },
            new User { Name = name2 }
        ];
        _ = await _repository.AddRangeAsync(users, TestContext.Current.CancellationToken);
        // Act
        await _repository.DeleteRangeAsync(users, TestContext.Current.CancellationToken);
        // Assert
        Assert.DoesNotContain(users[0], _repository.Entities);
        Assert.DoesNotContain(users[1], _repository.Entities);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingUser_ReturnsUser()
    {
        // Arrange
        User user = new() { Name = "Eve" };
        User added = await _repository.AddAsync(user, TestContext.Current.CancellationToken);

        // Act
        User? found = await _repository.GetByIdAsync(added.Id, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(found);
        Assert.Equal(added.Id, found!.Id);
    }

    // GetAllAsync
    [Fact]
    public async Task GetAllAsync_MultipleUsers_ReturnsAllUsers()
    {
        // Arrange
        List<User> users =
        [
            new User { Name = "Alice" },
            new User { Name = "Bob" }
        ];
        _ = await _repository.AddRangeAsync(users, TestContext.Current.CancellationToken);
        // Act
        IEnumerable<User> results = await _repository.GetAllAsync(TestContext.Current.CancellationToken);
        // Assert
        Assert.Contains(results, u => u.Name == "Alice");
        Assert.Contains(results, u => u.Name == "Bob");
    }

    // UpdateAsync
    [Fact]
    public async Task UpdateAsync_User_UserIsUpdated()
    {
        // Arrange
        User user = new() { Name = "Alice" };
        User added = await _repository.AddAsync(user, TestContext.Current.CancellationToken);
        added.Name = "Alice Updated";

        // Act
        await _repository.UpdateAsync(added, TestContext.Current.CancellationToken);

        // Assert
        User? updated = await _repository.GetByIdAsync(added.Id, TestContext.Current.CancellationToken);
        Assert.NotNull(updated);
        Assert.Equal("Alice Updated", updated!.Name);
    }

    // UpdateRangeAsync
    [Fact]
    public async Task UpdateRangeAsync_MultipleUsers_UsersAreUpdated()
    {
        // Arrange
        List<User> users =
        [
            new User { Name = "Alice" },
            new User { Name = "Bob" }
        ];
        IEnumerable<User> addedUsers = await _repository.AddRangeAsync(users, TestContext.Current.CancellationToken);
        List<User> updatedUsers = addedUsers.Select(u =>
        {
            u.Name += " Updated";
            return u;
        }).ToList();
        // Act
        await _repository.UpdateRangeAsync(updatedUsers, TestContext.Current.CancellationToken);
        // Assert
        foreach (User updated in updatedUsers)
        {
            User? found = await _repository.GetByIdAsync(updated.Id, TestContext.Current.CancellationToken);
            Assert.NotNull(found);
            Assert.EndsWith(" Updated", found!.Name);
        }
    }
}