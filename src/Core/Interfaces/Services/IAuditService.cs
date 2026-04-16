// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services;

public interface IAuditService
{
    Task LogAsync(string entityName, string changeType, string? changedBy, string description);
}
