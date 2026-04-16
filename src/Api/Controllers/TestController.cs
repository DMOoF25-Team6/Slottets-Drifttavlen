// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers;

/// <summary>
/// controller is used for testing the Audit Logging system.
/// manually trigger an audit log entry to verify that:
// -IAuditService is correctly injected via Dependency Injection
// -AuditService correctly creates and saves AuditLog entries to the database
// -The database connection and EF Core setup are working properly
/// </summary>

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly IAuditService _auditService;

    public TestController(IAuditService auditService)
    {
        _auditService = auditService;
    }

    [HttpPost("audit")]
    public async Task<IActionResult> CreateAuditTest()
    {

        // Calls the audit service to create a test audit log entry
        await _auditService.LogAsync(
            entityName: "User",
            changeType: "Created",
            changedBy: "test-user",
            description: "Test audit log entry from controller"
        );

        // Returns confirmation that the audit log was successfully created
        return Ok("Audit log created successfully");
    }
}
