using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarrativeSuite.Api.Data;

namespace NarrativeSuite.Api.Controllers;

[ApiController]
[Route("api/v1/projects/{slug}/metrics")]
public class MetricsController : ControllerBase
{
    private readonly AppDbContext _db;

    public MetricsController(AppDbContext db)
    {
        _db = db;
    }

    /// <summary>Get latest metrics (4 KPI cards)</summary>
    [HttpGet]
    public async Task<IActionResult> List(string slug)
    {
        var project = await _db.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Slug == slug);

        if (project is null)
            return NotFound(new { error = "Project not found" });

        // Get the latest scan that has metrics
        var latestScanId = await _db.Metrics
            .AsNoTracking()
            .Where(m => m.ProjectId == project.Id)
            .OrderByDescending(m => m.CalculatedAt)
            .Select(m => m.ScanId)
            .FirstOrDefaultAsync();

        if (latestScanId is null)
            return Ok(Array.Empty<object>());

        var metrics = await _db.Metrics
            .AsNoTracking()
            .Where(m => m.ProjectId == project.Id && m.ScanId == latestScanId)
            .Select(m => new
            {
                m.Id,
                m.MetricType,
                m.Value,
                m.Breakdown,
                m.CalculatedAt
            })
            .ToListAsync();

        return Ok(metrics);
    }

    /// <summary>Get single metric by type with breakdown</summary>
    [HttpGet("{type}")]
    public async Task<IActionResult> GetByType(string slug, string type)
    {
        var project = await _db.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Slug == slug);

        if (project is null)
            return NotFound(new { error = "Project not found" });

        var metric = await _db.Metrics
            .AsNoTracking()
            .Where(m => m.ProjectId == project.Id && m.MetricType == type)
            .OrderByDescending(m => m.CalculatedAt)
            .Select(m => new
            {
                m.Id,
                m.MetricType,
                m.Value,
                m.Breakdown,
                m.CalculatedAt
            })
            .FirstOrDefaultAsync();

        if (metric is null)
            return NotFound(new { error = $"Metric '{type}' not found" });

        return Ok(metric);
    }
}
