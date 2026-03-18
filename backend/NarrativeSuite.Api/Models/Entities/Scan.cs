namespace NarrativeSuite.Api.Models.Entities;

public class Scan
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProjectId { get; set; } = string.Empty;
    public string TriggerType { get; set; } = "manual";
    public string Status { get; set; } = "pending"; // pending, running, completed, failed
    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string? AiBriefing { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Project Project { get; set; } = null!;
    public ICollection<SerpResult> SerpResults { get; set; } = [];
    public ICollection<AiVisibility> AiVisibilities { get; set; } = [];
    public ICollection<TagScan> TagScans { get; set; } = [];
    public ICollection<Metric> Metrics { get; set; } = [];
    public ICollection<Insight> Insights { get; set; } = [];
    public ICollection<JobQueue> Jobs { get; set; } = [];
}
