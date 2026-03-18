namespace NarrativeSuite.Api.Models.Entities;

public class ProjectUser
{
    public string ProjectId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Role { get; set; } = "viewer"; // viewer, editor

    public Project Project { get; set; } = null!;
    public User User { get; set; } = null!;
}
