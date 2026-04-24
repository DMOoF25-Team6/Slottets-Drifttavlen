// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Core.Interfaces;
using Domain.Interfaces;

using Infrastructure.Data.Persistent;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

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
public abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
    /// </summary>
    /// <param name="context">An application database context used for entity operations.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="context"/> parameter is <see langword="null"/>.</exception>
    public Repository(AppDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    /// <inheritdoc/>
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _ = await _dbSet.AddAsync(entity, cancellationToken);
        _ = await _context.SaveChangesAsync(cancellationToken);

        TEntity? persisted = await _dbSet.FindAsync([entity.Id], cancellationToken);
        return await Task.FromResult(persisted) ?? throw new Exception("Entity not found after adding.");
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entities);
        await _dbSet.AddRangeAsync(entities, cancellationToken);
        _ = await _context.SaveChangesAsync(cancellationToken);
        return entities;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _ = _dbSet.Remove(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entities);
        foreach (TEntity entity in entities)
        {
            _ = _dbSet.Remove(entity);
        }
        _ = await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _ = _dbSet.Update(entity);
        _ = await _context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entities);
        foreach (TEntity entity in entities)
        {
            _ = _dbSet.Update(entity);
        }
        _ = await _context.SaveChangesAsync(cancellationToken);
    }
}
