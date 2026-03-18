using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarrativeSuite.Api.Data;

namespace NarrativeSuite.Api.Controllers;

[ApiController]
[Route("api/v1/projects/{slug}/insights")]
public class InsightsController : ControllerBase
{
    private readonly AppDbContext _db;

    public InsightsController(AppDbContext db)
    {
        _db = db;
    }

    /// <summary>Get insights ordered by score descending</summary>
    [HttpGet]
    public async Task<IActionResult> List(string slug)
    {
        var project = await _db.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Slug == slug);

        if (project is null)
            return NotFound(new { error = "Project not found" });

        var insights = await _db.Insights
            .AsNoTracking()
            .Where(i => i.ProjectId == project.Id)
            .OrderByDescending(i => i.Score)
            .Select(i => new
            {
                i.Id,
                i.Type,
                i.Title,
                i.Description,
                i.Score,
                i.DataEvidence,
                i.Status,
                i.CreatedAt
            })
            .ToListAsync();

        return Ok(insights);
    }
}
