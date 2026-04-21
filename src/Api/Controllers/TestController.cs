// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers;


/// <summary>
/// Provides endpoints for testing the Audit Logging system.
/// </summary>
/// <remarks>
/// This controller allows manual triggering of an audit log entry to verify:
/// <list type="bullet">
/// <item><description><see cref="IAuditService"/> is correctly injected via dependency injection.</description></item>
/// <item><description>AuditService correctly creates and saves <c>AuditLog</c> entries to the database.</description></item>
/// <item><description>The database connection and EF Core setup are working properly.</description></item>
/// </list>
/// </remarks>

[ApiController]
[Route("api/test")]
public class TestController(IAuditService auditService) : ControllerBase
{
    /// <summary>
    /// Creates a test audit log entry to verify the audit logging system.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
    [HttpPost("audit")]
    public async Task<IActionResult> CreateAuditTest()
    {

        // Calls the audit service to create a test audit log entry
        await auditService.LogAsync(
            entityName: "User",
            changeType: "Created",
            changedBy: "test-user",
            description: "Test audit log entry from controller"
        );

        // Returns confirmation that the audit log was successfully created
        return Ok("Audit log created successfully");
    }
}
