namespace NarrativeSuite.Api.Models.Entities;

public class JobQueue
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ScanId { get; set; } = string.Empty;
    public string JobType { get; set; } = string.Empty; // serp, news, ai_overview, llm_fanout, analysis, metrics, briefing
    public string Keyword { get; set; } = string.Empty;
    public string Status { get; set; } = "pending"; // pending, processing, completed, failed
    public int RetryCount { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    public Scan Scan { get; set; } = null!;
}
