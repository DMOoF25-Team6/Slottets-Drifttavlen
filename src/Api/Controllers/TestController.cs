// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

#if DEBUG
using Core.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Provides endpoints for testing the Audit Logging system.
/// Only available in DEBUG builds — excluded from production.
/// </summary>
[ApiController]
[Route("api/test")]
public class TestController(IAuditService auditService) : ControllerBase
{
    /// <summary>
    /// Creates a test audit log entry to verify the audit logging system.
    /// </summary>
    [HttpPost("audit")]
    public async Task<IActionResult> CreateAuditTest()
    {
        await auditService.LogAsync(
            entityName: "User",
            changeType: "Created",
            changedBy: "test-user",
            description: "Test audit log entry from controller"
        );

        return Ok("Audit log created successfully");
    }
}
#endif
