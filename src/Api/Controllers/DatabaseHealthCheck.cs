// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Infrastructure.Data.Persistent;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class DatabaseHealthCheck : ControllerBase
{
    [HttpGet("IsDbConnected")]
    public IActionResult IsConnected()
    {
        // Simulate a database connection check
        bool isConnected = CheckDatabaseConnection();
        if (isConnected)
        {
            return Ok(new { status = "Database is connected" });
        }
        else
        {
            return StatusCode(503, new { status = "Database is not connected" });
        }
    }

    private bool CheckDatabaseConnection()
    {
        // get DbContext from DI container
        using IServiceScope scope = HttpContext.RequestServices.CreateScope();
        AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        try
        {
            _ = dbContext.Database.CanConnect();
        }
        catch
        {
            return false;
        }
        return true;
    }
}