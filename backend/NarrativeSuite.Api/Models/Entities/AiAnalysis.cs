namespace NarrativeSuite.Api.Models.Entities;

public class AiAnalysis
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string SerpResultId { get; set; } = string.Empty;
    public string Themes { get; set; } = "[]"; // JSON array
    public string Sentiment { get; set; } = "neutral"; // positive, negative, neutral, mixed
    public float SentimentScore { get; set; }
    public string Entities { get; set; } = "[]"; // JSON array
    public string Summary { get; set; } = string.Empty;
    public string LanguageDetected { get; set; } = string.Empty;
    public bool IsPriority { get; set; }
    public DateTime AnalyzedAt { get; set; } = DateTime.UtcNow;

    public SerpResult SerpResult { get; set; } = null!;
}
