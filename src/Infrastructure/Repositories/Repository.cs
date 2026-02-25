// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Interfaces;

namespace Infrastructure.Repositories;

/// <summary>
/// Abstract base class for repository implementations in the Infrastructure layer.
/// Implements the <see cref="IRepository{TEntity}"/> interface for managing entities of type <typeparamref name="TEntity"/>.
/// 
/// <para>
/// This repository provides in-memory CRUD operations for entities implementing <see cref="IEntity"/>.
/// Each entity is assigned a new <see cref="Guid"/> as its Id when added.
/// </para>
/// <para>
/// Intended for use as a base class for concrete repositories, following Clean Architecture principles.
/// </para>
/// <remarks>
/// - Entities are stored in an <see cref="IEnumerable{TEntity}"/> property, which is updated on each operation.
/// - This implementation is suitable for testing or prototyping; for production, use a persistent data store.
/// - All methods are asynchronous to support future extensibility and integration with async data sources.
/// </remarks>
/// <example>
/// Example usage:
/// <code>
/// public class UserRepository : Repository&lt;User&gt; { }
/// </code>
/// </example>
/// </summary>
public abstract class Repository<TEntity>() : IRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <inheritdoc/>
    public IEnumerable<TEntity> Entities { get; set; } = [];

    /// <inheritdoc/>
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.Id = Guid.NewGuid();
        Entities = Entities.Append(entity);
        return entity;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        List<TEntity> entitiesList = entities.ToList();
        foreach (TEntity entity in entitiesList)
        {
            entity.Id = Guid.NewGuid();
            Entities = Entities.Append(entity);
        }
        return entitiesList.AsEnumerable();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Entities = Entities.Where(e => e.Id != entity.Id);
    }

    /// <inheritdoc/>
    public async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (TEntity entity in entities)
        {
            Entities = Entities.Where(e => e.Id != entity.Id);
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Entities;
    }

    /// <inheritdoc/>
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Entities.FirstOrDefault(e => e.Id == id);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Entities = Entities.Select(e => e.Id == entity.Id ? entity : e);
    }

    /// <inheritdoc/>
    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        foreach (TEntity entity in entities)
        {
            Entities = Entities.Select(e => e.Id == entity.Id ? entity : e);
        }
    }
}
