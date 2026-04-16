// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class AuditLog
{

    public Guid Id { get; set; }
    public string EntityName { get; set; } = string.Empty;
    public string ChangeType { get; set; } = string.Empty;
    public string? ChangedBy { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Description { get; set; } = string.Empty;


}
