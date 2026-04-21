// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

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
public abstract class Repository<TEntity>(AppDbContext context) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    protected readonly AppDbContext _context = context;

    /// <inheritdoc/>
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await using AppDbContext ctx = _context;
        DbSet<TEntity> dbSet = ctx.Set<TEntity>();
        _ = await dbSet.AddAsync(entity, cancellationToken);
        _ = await ctx.SaveChangesAsync(cancellationToken);

        // Reload the persisted entity from the database so any DB-generated values (like Id) are populated.
        TEntity? persisted = await dbSet.FindAsync([entity.Id], cancellationToken);
        return await Task.FromResult(persisted) ?? entity;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await using AppDbContext ctx = _context;
        DbSet<TEntity> dbSet = ctx.Set<TEntity>();
        await dbSet.AddRangeAsync(entities, cancellationToken);
        _ = await ctx.SaveChangesAsync(cancellationToken);
        return await Task.FromResult(entities);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await using AppDbContext ctx = _context;
        DbSet<TEntity> dbSet = ctx.Set<TEntity>();
        _ = dbSet.Remove(entity);
        _ = await ctx.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await using AppDbContext ctx = _context;
        DbSet<TEntity> dbSet = ctx.Set<TEntity>();
        foreach (TEntity entity in entities)
        {
            _ = dbSet.Remove(entity);
        }
        _ = await ctx.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await using AppDbContext ctx = _context;
        DbSet<TEntity> dbSet = ctx.Set<TEntity>();
        return await dbSet.ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using AppDbContext ctx = _context;
        DbSet<TEntity> dbSet = ctx.Set<TEntity>();
        return await dbSet.FindAsync([id], cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await using AppDbContext ctx = _context;
        DbSet<TEntity> dbSet = ctx.Set<TEntity>();
        _ = dbSet.Update(entity);
        _ = await ctx.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await using AppDbContext ctx = _context;
        DbSet<TEntity> dbSet = ctx.Set<TEntity>();
        foreach (TEntity entity in entities)
        {
            _ = dbSet.Update(entity);
        }
        _ = await ctx.SaveChangesAsync(cancellationToken);
    }
}
