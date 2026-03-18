using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarrativeSuite.Api.Data;

namespace NarrativeSuite.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HealthController : ControllerBase
{
    private readonly AppDbContext _db;

    public HealthController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var canConnect = false;
        try
        {
            canConnect = await _db.Database.CanConnectAsync();
        }
        catch
        {
            // Database not available
        }

        return Ok(new
        {
            status = "ok",
            timestamp = DateTime.UtcNow,
            database = canConnect ? "connected" : "unavailable"
        });
    }
}
