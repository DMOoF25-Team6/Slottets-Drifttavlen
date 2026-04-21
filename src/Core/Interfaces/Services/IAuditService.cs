// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services;

/// <summary>
/// Defines a contract for logging audit events in the system.
/// </summary>
/// <remarks>
/// Implementations should persist audit logs for entity changes, including the type of change, the user responsible, and a description.
/// </remarks>
public interface IAuditService
{
    /// <summary>
    /// Logs an audit event for a change to an entity.
    /// </summary>
    /// <param name="entityName">A name of the entity being changed.</param>
    /// <param name="changeType">A type of change (e.g., Created, Updated, Deleted).</param>
    /// <param name="changedBy">A user or process responsible for the change, or <see langword="null"/> if unknown.</param>
    /// <param name="description">A description of the change.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task LogAsync(string entityName, string changeType, string? changedBy, string description);
}
