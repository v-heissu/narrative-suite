using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarrativeSuite.Api.Data;

namespace NarrativeSuite.Api.Controllers;

[ApiController]
[Route("api/v1/projects/{slug}")]
public class ResultsController : ControllerBase
{
    private readonly AppDbContext _db;

    public ResultsController(AppDbContext db)
    {
        _db = db;
    }

    /// <summary>Get SERP results with AI analysis (paginated, filterable)</summary>
    [HttpGet("results")]
    public async Task<IActionResult> GetResults(
        string slug,
        [FromQuery] string? keyword = null,
        [FromQuery] string? source = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var project = await _db.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Slug == slug);

        if (project is null)
            return NotFound(new { error = "Project not found" });

        // Get latest completed scan
        var latestScan = await _db.Scans
            .AsNoTracking()
            .Where(s => s.ProjectId == project.Id && s.Status == "completed")
            .OrderByDescending(s => s.CompletedAt)
            .FirstOrDefaultAsync();

        if (latestScan is null)
            return Ok(new { items = Array.Empty<object>(), total = 0, page, pageSize });

        var query = _db.SerpResults
            .AsNoTracking()
            .Include(sr => sr.AiAnalysis)
            .Where(sr => sr.ScanId == latestScan.Id);

        if (!string.IsNullOrWhiteSpace(keyword))
            query = query.Where(sr => sr.Keyword == keyword);

        if (!string.IsNullOrWhiteSpace(source))
            query = query.Where(sr => sr.Source == source);

        var total = await query.CountAsync();

        var results = await query
            .OrderBy(sr => sr.Keyword)
            .ThenBy(sr => sr.Position)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(sr => new
            {
                sr.Id,
                sr.Keyword,
                sr.Source,
                sr.Position,
                sr.Url,
                sr.Title,
                sr.Snippet,
                sr.Domain,
                sr.IsCompetitor,
                sr.FetchedAt,
                aiAnalysis = sr.AiAnalysis == null ? null : new
                {
                    sr.AiAnalysis.Id,
                    sr.AiAnalysis.Themes,
                    sr.AiAnalysis.Sentiment,
                    sr.AiAnalysis.SentimentScore,
                    sr.AiAnalysis.Entities,
                    sr.AiAnalysis.Summary,
                    sr.AiAnalysis.LanguageDetected,
                    sr.AiAnalysis.IsPriority
                }
            })
            .ToListAsync();

        return Ok(new { items = results, total, page, pageSize });
    }

    /// <summary>Get AI visibility results</summary>
    [HttpGet("ai-visibility")]
    public async Task<IActionResult> GetAiVisibility(string slug)
    {
        var project = await _db.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Slug == slug);

        if (project is null)
            return NotFound(new { error = "Project not found" });

        // Get latest completed scan
        var latestScan = await _db.Scans
            .AsNoTracking()
            .Where(s => s.ProjectId == project.Id && s.Status == "completed")
            .OrderByDescending(s => s.CompletedAt)
            .FirstOrDefaultAsync();

        if (latestScan is null)
            return Ok(Array.Empty<object>());

        var results = await _db.AiVisibilities
            .AsNoTracking()
            .Where(av => av.ScanId == latestScan.Id)
            .OrderBy(av => av.Keyword)
            .ThenBy(av => av.LlmModel)
            .Select(av => new
            {
                av.Id,
                av.Keyword,
                av.LlmModel,
                av.ResponseText,
                av.BrandMentioned,
                av.BrandSentiment,
                av.CompetitorMentions,
                av.VisibilityScore,
                av.TopicsCovered,
                av.QueriedAt
            })
            .ToListAsync();

        return Ok(results);
    }
}
