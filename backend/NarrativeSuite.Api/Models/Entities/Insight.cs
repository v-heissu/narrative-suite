namespace NarrativeSuite.Api.Models.Entities;

public class Insight
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProjectId { get; set; } = string.Empty;
    public string ScanId { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // gap, opportunity, risk, trend
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public float Score { get; set; }
    public string DataEvidence { get; set; } = "{}"; // JSON
    public string Status { get; set; } = "new"; // new, acknowledged, in_plan, done
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Project Project { get; set; } = null!;
    public Scan Scan { get; set; } = null!;
    public ICollection<EditorialPlan> EditorialPlans { get; set; } = [];
}
