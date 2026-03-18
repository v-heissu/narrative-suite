namespace NarrativeSuite.Api.Models.Entities;

public class Metric
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProjectId { get; set; } = string.Empty;
    public string ScanId { get; set; } = string.Empty;
    public string MetricType { get; set; } = string.Empty; // nsov, tos, nri, tai
    public float Value { get; set; }
    public string Breakdown { get; set; } = "{}"; // JSON
    public DateTime CalculatedAt { get; set; } = DateTime.UtcNow;

    public Project Project { get; set; } = null!;
    public Scan Scan { get; set; } = null!;
}
