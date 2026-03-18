using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarrativeSuite.Api.Data;

namespace NarrativeSuite.Api.Controllers;

[ApiController]
[Route("api/v1/projects")]
public class ProjectsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProjectsController(AppDbContext db)
    {
        _db = db;
    }

    /// <summary>List all projects</summary>
    [HttpGet]
    public async Task<IActionResult> List()
    {
        var projects = await _db.Projects
            .AsNoTracking()
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new
            {
                p.Id,
                p.Slug,
                p.Name,
                p.Industry,
                p.BrandName,
                p.IsActive
            })
            .ToListAsync();

        return Ok(projects);
    }

    /// <summary>Get single project by slug with full details</summary>
    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var project = await _db.Projects
            .AsNoTracking()
            .Where(p => p.Slug == slug)
            .Select(p => new
            {
                p.Id,
                p.Slug,
                p.Name,
                p.Industry,
                p.BrandName,
                p.Keywords,
                p.Competitors,
                p.AiCompetitors,
                p.Sources,
                p.Schedule,
                p.ScheduleDay,
                p.Language,
                p.LocationCode,
                p.Context,
                p.AlertKeywords,
                p.IsActive,
                p.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (project is null)
            return NotFound(new { error = "Project not found" });

        return Ok(project);
    }
}
