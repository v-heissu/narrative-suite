namespace NarrativeSuite.Api.Models.Entities;

public class ContentDraft
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ContentBriefId { get; set; } = string.Empty;
    public string ContentHtml { get; set; } = string.Empty;
    public float? VoiceCheckScore { get; set; }
    public string? VoiceCheckDetails { get; set; } // JSON
    public string? SeoCheck { get; set; } // JSON
    public int Version { get; set; } = 1;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ContentBrief ContentBrief { get; set; } = null!;
}
