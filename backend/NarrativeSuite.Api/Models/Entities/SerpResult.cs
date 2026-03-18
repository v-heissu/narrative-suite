namespace NarrativeSuite.Api.Models.Entities;

public class SerpResult
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ScanId { get; set; } = string.Empty;
    public string Keyword { get; set; } = string.Empty;
    public string Source { get; set; } = "google_organic"; // google_organic, google_news, ai_overview
    public int Position { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Snippet { get; set; } = string.Empty;
    public string Domain { get; set; } = string.Empty;
    public bool IsCompetitor { get; set; }
    public string? Excerpt { get; set; }
    public DateTime FetchedAt { get; set; } = DateTime.UtcNow;

    public Scan Scan { get; set; } = null!;
    public AiAnalysis? AiAnalysis { get; set; }
}
