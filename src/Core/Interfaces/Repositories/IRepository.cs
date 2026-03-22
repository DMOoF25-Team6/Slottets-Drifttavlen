// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Interfaces;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Defines a generic repository interface for CRUD operations on entities.
/// <para>
/// This interface follows the Repository pattern, providing abstraction over data access logic.
/// It is intended for use in the Core layer of Clean Architecture, ensuring separation of business logic from infrastructure concerns.
/// </para>
/// <typeparam name="TEntity">
/// The entity type managed by the repository. Must implement <see cref="Domain.Interfaces.IEntity"/>.
/// </typeparam>
/// <remarks>
/// - All operations are asynchronous and support cancellation via <see cref="CancellationToken"/>.
/// - Implementations should handle persistence and retrieval of entities, typically using Entity Framework Core or another ORM.
/// - This interface is designed for extensibility and testability.
/// </remarks>
/// <example>
/// <code>
/// public class UserRepository : IRepository&lt;User&gt; { ... }
/// </code>
/// </example>
public interface IRepository<TEntity> : ICrud<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// Gets or sets the collection of entities managed by the repository.
    /// </summary>
    IEnumerable<TEntity> Entities { get; set; }
}
