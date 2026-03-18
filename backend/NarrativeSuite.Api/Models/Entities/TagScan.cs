namespace NarrativeSuite.Api.Models.Entities;

public class TagScan
{
    public string TagId { get; set; } = string.Empty;
    public string ScanId { get; set; } = string.Empty;
    public int Count { get; set; }
    public float AvgSentiment { get; set; }

    public Tag Tag { get; set; } = null!;
    public Scan Scan { get; set; } = null!;
}
