namespace NarrativeSuite.Api.Models.Entities;

public class AiVisibility
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ScanId { get; set; } = string.Empty;
    public string Keyword { get; set; } = string.Empty;
    public string LlmModel { get; set; } = string.Empty; // chatgpt, gemini, perplexity, grok, claude
    public string ResponseText { get; set; } = string.Empty;
    public bool BrandMentioned { get; set; }
    public float? BrandSentiment { get; set; }
    public string? CompetitorMentions { get; set; } // JSON array
    public float VisibilityScore { get; set; }
    public string? TopicsCovered { get; set; } // JSON array
    public DateTime QueriedAt { get; set; } = DateTime.UtcNow;

    public Scan Scan { get; set; } = null!;
}
