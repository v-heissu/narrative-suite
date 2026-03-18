namespace NarrativeSuite.Api.Models.Entities;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "client"; // admin, client
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ProjectUser> ProjectUsers { get; set; } = [];
}
