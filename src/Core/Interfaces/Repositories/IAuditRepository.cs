// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Interface for saving audit logs to the database.
/// 
/// The implementation is handled in Infrastructure.Data.
/// </summary>

public interface IAuditRepository
{
    Task AddAsync(AuditLog log);


}
