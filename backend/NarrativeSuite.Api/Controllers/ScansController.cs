using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarrativeSuite.Api.Data;

namespace NarrativeSuite.Api.Controllers;

[ApiController]
[Route("api/v1/projects/{slug}/scans")]
public class ScansController : ControllerBase
{
    private readonly AppDbContext _db;

    public ScansController(AppDbContext db)
    {
        _db = db;
    }

    /// <summary>List scans for a project</summary>
    [HttpGet]
    public async Task<IActionResult> List(string slug)
    {
        var project = await _db.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Slug == slug);

        if (project is null)
            return NotFound(new { error = "Project not found" });

        var scans = await _db.Scans
            .AsNoTracking()
            .Where(s => s.ProjectId == project.Id)
            .OrderByDescending(s => s.CreatedAt)
            .Select(s => new
            {
                s.Id,
                s.Status,
                s.TriggerType,
                s.TotalTasks,
                s.CompletedTasks,
                s.StartedAt,
                s.CompletedAt,
                s.CreatedAt
            })
            .ToListAsync();

        return Ok(scans);
    }

    /// <summary>Get the latest completed scan with AI briefing</summary>
    [HttpGet("latest")]
    public async Task<IActionResult> Latest(string slug)
    {
        var project = await _db.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Slug == slug);

        if (project is null)
            return NotFound(new { error = "Project not found" });

        var scan = await _db.Scans
            .AsNoTracking()
            .Where(s => s.ProjectId == project.Id && s.Status == "completed")
            .OrderByDescending(s => s.CompletedAt)
            .Select(s => new
            {
                s.Id,
                s.Status,
                s.TriggerType,
                s.TotalTasks,
                s.CompletedTasks,
                s.DateFrom,
                s.DateTo,
                s.AiBriefing,
                s.StartedAt,
                s.CompletedAt,
                s.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (scan is null)
            return NotFound(new { error = "No completed scan found" });

        return Ok(scan);
    }
}
