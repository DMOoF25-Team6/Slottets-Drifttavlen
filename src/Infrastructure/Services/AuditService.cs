// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Services;

using Domain.Entities;



namespace Infrastructure.Services;

/// <summary>
///  Service responsible for writing audit logs to the database.
/// Each log entry contains information about the entity changed
/// the type of operation performed, the user responsible
/// and a timestamp of when the change occurred.
/// </summary>
internal class AuditService : IAuditService
{

    //TODO: Fix this to use a mangager for an api call to the database instead of direct access to the dbcontext, this is to avoid tight coupling and make it easier to test and maintain in the future.
    //private readonly AppDbContext _dbContext;

    //public AuditService(AppDbContext dbContext)
    //{
    //    _dbContext = dbContext;
    //}

    public async Task LogAsync(string entityName, string changeType, string? changedBy, string description)
    {
        _ = new AuditLog()
        {
            Id = Guid.NewGuid(),
            EntityName = entityName,
            ChangeType = changeType,
            ChangedBy = changedBy ?? "system",
            Timestamp = DateTime.UtcNow,
            Description = description
        };

        //_dbContext.AuditLogs.Add(log);
        //await _dbContext.SaveChangesAsync();
        await Task.CompletedTask; // Placeholder for async operation
    }
}
