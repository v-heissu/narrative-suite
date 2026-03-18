namespace NarrativeSuite.Api.Models.Entities;

public class EditorialPlan
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProjectId { get; set; } = string.Empty;
    public string? InsightId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? KeywordTarget { get; set; }
    public string Status { get; set; } = "idea"; // idea, briefed, writing, review, published
    public string? AssignedTo { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Project Project { get; set; } = null!;
    public Insight? Insight { get; set; }
    public ICollection<ContentBrief> ContentBriefs { get; set; } = [];
}
