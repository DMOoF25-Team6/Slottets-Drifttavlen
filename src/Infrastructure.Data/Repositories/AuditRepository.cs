// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Data.Persistent;


namespace Infrastructure.Data.Repositories;

/// <summary>
///  Repository responsible for persisting audit log entries to the database.
/// </summary>

public class AuditRepository(AppDbContext context) : IAuditRepository
{

    private readonly AppDbContext _context = context;


    //Adds a new audit log entry to the database.
    public async Task AddAsync(AuditLog log)
    {
        _ = _context.AuditLogs.Add(log);
        _ = await _context.SaveChangesAsync();

    }


}
