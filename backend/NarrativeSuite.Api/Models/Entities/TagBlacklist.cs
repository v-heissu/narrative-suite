namespace NarrativeSuite.Api.Models.Entities;

public class TagBlacklist
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProjectId { get; set; } = string.Empty;
    public string TagName { get; set; } = string.Empty;

    public Project Project { get; set; } = null!;
}
