// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Infrastructure.Data.Helpers;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("/[controller]")]
[ApiController]
public class DatabaseController(IServiceProvider serviceProvider) : ControllerBase
{
    [HttpGet("IsDbConnected")]
    public IActionResult IsConnected()
    {
        // Simulate a database connection check
        bool isConnected = DatabaseConnection.IsConnected(serviceProvider);
        if (isConnected)
        {
            return Ok(new { status = "Database is connected" });
        }
        else
        {
            return StatusCode(503, new { status = "Database is not connected" });
        }
    }
}
