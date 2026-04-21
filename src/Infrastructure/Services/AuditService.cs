// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Interfaces.Repositories;

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

   private readonly IAuditRepository _repository;

    public AuditService(IAuditRepository repository)
    {
        _repository = repository;
    }   




    public async Task LogAsync(string entityName, string changeType, string? changedBy, string description)
    {
        var log = new AuditLog()
        {
            Id = Guid.NewGuid(),
            EntityName = entityName,
            ChangeType = changeType,
            ChangedBy = changedBy ?? "system",
            Timestamp = DateTime.UtcNow,
            Description = description
        };

        await _repository.AddAsync(log);

    }
}
    