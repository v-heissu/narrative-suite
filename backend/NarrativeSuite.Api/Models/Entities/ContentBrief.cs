namespace NarrativeSuite.Api.Models.Entities;

public class ContentBrief
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string EditorialPlanId { get; set; } = string.Empty;
    public string KeywordTarget { get; set; } = string.Empty;
    public string SerpContext { get; set; } = "{}"; // JSON
    public string AiContext { get; set; } = "{}"; // JSON
    public string SuggestedStructure { get; set; } = "{}"; // JSON
    public string Subtopics { get; set; } = "[]"; // JSON
    public int? TargetLengthWords { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public EditorialPlan EditorialPlan { get; set; } = null!;
    public ICollection<ContentDraft> ContentDrafts { get; set; } = [];
}
