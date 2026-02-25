// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

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
public interface IRepository<TEntity> where TEntity : Domain.Interfaces.IEntity
{
    /// <summary>
    /// Gets or sets the collection of entities managed by the repository.
    /// </summary>
    IEnumerable<TEntity> Entities { get; set; }

    #region Create operations
    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>The added entity.</returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds multiple entities to the repository.
    /// </summary>
    /// <param name="entities">The entities to add.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>The added entities.</returns>
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    #endregion

    #region Read operations
    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>The entity if found; otherwise, <c>null</c>.</returns>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all entities from the repository.
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A collection of all entities.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    #endregion

    #region Update operations
    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates multiple entities in the repository.
    /// </summary>
    /// <param name="entities">The entities to update.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    #endregion

    #region Delete operations
    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple entities from the repository.
    /// </summary>
    /// <param name="entities">The entities to delete.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    #endregion
}
