// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Data.Persistent;


namespace Infrastructure.Data.Repositories;

/// <summary>
///  Repository responsible for persisting audit log entries to the database.
/// </summary>

public class AuditRepository : IAuditRepository
{

    private readonly AppDbContext _context;

    public AuditRepository(AppDbContext context)
    {
        _context = context;
    }


    //Adds a new audit log entry to the database.
    public async Task AddAsync(AuditLog log)
    {
        _context.AuditLogs.Add(log);
        await _context.SaveChangesAsync();

    }


}
