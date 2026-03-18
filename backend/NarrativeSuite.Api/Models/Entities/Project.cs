namespace NarrativeSuite.Api.Models.Entities;

public class Project
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string Keywords { get; set; } = "[]"; // JSON array
    public string Competitors { get; set; } = "[]"; // JSON array
    public string AiCompetitors { get; set; } = "[]"; // JSON array
    public string Sources { get; set; } = "[\"google_organic\",\"google_news\"]"; // JSON array
    public string Schedule { get; set; } = "manual"; // weekly, monthly, manual
    public int ScheduleDay { get; set; } = 1;
    public string Language { get; set; } = "it";
    public int LocationCode { get; set; } = 2380; // DataForSEO: Italy
    public string? Context { get; set; }
    public string? AlertKeywords { get; set; } // JSON array
    public string? RagflowDatasetId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ProjectUser> ProjectUsers { get; set; } = [];
    public ICollection<Scan> Scans { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];
    public ICollection<Metric> Metrics { get; set; } = [];
    public ICollection<Insight> Insights { get; set; } = [];
    public ICollection<EditorialPlan> EditorialPlans { get; set; } = [];
    public ICollection<TagBlacklist> TagBlacklists { get; set; } = [];
}
