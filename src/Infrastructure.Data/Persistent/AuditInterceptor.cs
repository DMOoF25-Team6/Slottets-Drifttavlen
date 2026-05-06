// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System.Reflection;

using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Data.Persistent;

public class AuditInterceptor : SaveChangesInterceptor
{
    private IEnumerable<AuditEntry>? _auditEntries;

    public AuditInterceptor(IEnumerable<AuditEntry>? auditEntries = null)
    {
        _auditEntries = auditEntries;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        DateTime startTime = DateTime.UtcNow;

        var auditEntries = eventData.Context.ChangeTracker
            .Entries()
            .Where(e => (
                e.Entity is not AuditEntry
                && e.State == EntityState.Added)
                || e.State == EntityState.Modified
                || e.State == EntityState.Deleted)
            .Select(e => new AuditEntry
            {
                Id = Guid.NewGuid(),
                StartTimeUtc = startTime,
                Metadata = $"{e.Entity.GetType().Name} - {e.State}",
                UserId = GetUserIdFromToken(eventData.Context), // Get user from token
            })
            .ToList();

        if (!auditEntries.Any())
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        _auditEntries = _auditEntries is not null
            ? _auditEntries.Concat(auditEntries)
            : auditEntries;

        await eventData.Context.AddRangeAsync(auditEntries, cancellationToken);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        DateTime endTime = DateTime.UtcNow;

        foreach (AuditEntry entry in _auditEntries ?? [])
        {
            entry.EndTimeUtc = endTime;
            entry.Succeeded = true;
        }

        // Removed duplicate AddRangeAsync and SaveChangesAsync to prevent duplicate key exception
        _auditEntries = null;

        // After saving changes, you can perform additional actions if needed
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    public override async Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        // Handle any errors that occur during SaveChanges
        await base.SaveChangesFailedAsync(eventData, cancellationToken);
    }

    private Guid GetUserIdFromToken(DbContext context)
    {
        // Try to get the user ID from the context property
        PropertyInfo? property = context.GetType().GetProperty("CurrentUserId");
        if (property is not null && property.PropertyType == typeof(Guid))
        {
            object? value = property.GetValue(context);
            if (value is Guid guid && guid != Guid.Empty)
            {
                return guid;
            }
        }
        // Fallback if not set
        return Guid.Empty;
    }
}
