// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly IDatabaseService _databaseService;

    public DatabaseController(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    /// <summary>
    /// Checks if the database connection is available.
    /// </summary>
    /// <returns>True if connected, otherwise false.</returns>
    [HttpGet("isconnected")]
    public ActionResult<bool> IsConnected()
    {
        return Ok(_databaseService.IsConnected());
    }
}
