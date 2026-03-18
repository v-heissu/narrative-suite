using Microsoft.EntityFrameworkCore;
using NarrativeSuite.Api.Models.Entities;

namespace NarrativeSuite.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectUser> ProjectUsers => Set<ProjectUser>();
    public DbSet<Scan> Scans => Set<Scan>();
    public DbSet<SerpResult> SerpResults => Set<SerpResult>();
    public DbSet<AiAnalysis> AiAnalyses => Set<AiAnalysis>();
    public DbSet<AiVisibility> AiVisibilities => Set<AiVisibility>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<TagScan> TagScans => Set<TagScan>();
    public DbSet<TagBlacklist> TagBlacklists => Set<TagBlacklist>();
    public DbSet<Metric> Metrics => Set<Metric>();
    public DbSet<Insight> Insights => Set<Insight>();
    public DbSet<EditorialPlan> EditorialPlans => Set<EditorialPlan>();
    public DbSet<ContentBrief> ContentBriefs => Set<ContentBrief>();
    public DbSet<ContentDraft> ContentDrafts => Set<ContentDraft>();
    public DbSet<JobQueue> JobQueues => Set<JobQueue>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- User ---
        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("users");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.Email).HasMaxLength(255).IsRequired();
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.DisplayName).HasMaxLength(255);
            e.Property(x => x.PasswordHash).HasMaxLength(255).IsRequired();
            e.Property(x => x.Role).HasMaxLength(20).IsRequired();
        });

        // --- Project ---
        modelBuilder.Entity<Project>(e =>
        {
            e.ToTable("projects");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.Slug).HasMaxLength(100).IsRequired();
            e.HasIndex(x => x.Slug).IsUnique();
            e.Property(x => x.Name).HasMaxLength(255).IsRequired();
            e.Property(x => x.Industry).HasMaxLength(255);
            e.Property(x => x.BrandName).HasMaxLength(255);
            e.Property(x => x.Keywords).HasColumnType("longtext").IsRequired();
            e.Property(x => x.Competitors).HasColumnType("longtext").IsRequired();
            e.Property(x => x.AiCompetitors).HasColumnType("longtext").IsRequired();
            e.Property(x => x.Sources).HasColumnType("longtext").IsRequired();
            e.Property(x => x.Schedule).HasMaxLength(20).IsRequired();
            e.Property(x => x.Language).HasMaxLength(10);
            e.Property(x => x.Context).HasColumnType("text");
            e.Property(x => x.AlertKeywords).HasColumnType("longtext");
            e.Property(x => x.RagflowDatasetId).HasMaxLength(255);
        });

        // --- ProjectUser ---
        modelBuilder.Entity<ProjectUser>(e =>
        {
            e.ToTable("project_users");
            e.HasKey(x => new { x.ProjectId, x.UserId });
            e.Property(x => x.ProjectId).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.UserId).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.Role).HasMaxLength(20).IsRequired();
            e.HasOne(x => x.Project).WithMany(p => p.ProjectUsers).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.User).WithMany(u => u.ProjectUsers).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        // --- Scan ---
        modelBuilder.Entity<Scan>(e =>
        {
            e.ToTable("scans");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.ProjectId).HasMaxLength(36).IsFixedLength().IsRequired();
            e.Property(x => x.TriggerType).HasMaxLength(50);
            e.Property(x => x.Status).HasMaxLength(20).IsRequired();
            e.Property(x => x.AiBriefing).HasColumnType("text");
            e.HasOne(x => x.Project).WithMany(p => p.Scans).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
        });

        // --- SerpResult ---
        modelBuilder.Entity<SerpResult>(e =>
        {
            e.ToTable("serp_results");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.ScanId).HasMaxLength(36).IsFixedLength().IsRequired();
            e.Property(x => x.Keyword).HasMaxLength(500).IsRequired();
            e.Property(x => x.Source).HasMaxLength(30).IsRequired();
            e.Property(x => x.Url).HasColumnType("text").IsRequired();
            e.Property(x => x.Title).HasColumnType("text").IsRequired();
            e.Property(x => x.Snippet).HasColumnType("text").IsRequired();
            e.Property(x => x.Domain).HasMaxLength(500);
            e.Property(x => x.Excerpt).HasColumnType("text");
            e.HasIndex(x => new { x.ScanId, x.Keyword });
            e.HasIndex(x => x.Domain);
            e.HasIndex(x => x.IsCompetitor);
            e.HasOne(x => x.Scan).WithMany(s => s.SerpResults).HasForeignKey(x => x.ScanId).OnDelete(DeleteBehavior.Cascade);
        });

        // --- AiAnalysis ---
        modelBuilder.Entity<AiAnalysis>(e =>
        {
            e.ToTable("ai_analysis");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.SerpResultId).HasMaxLength(36).IsFixedLength().IsRequired();
            e.Property(x => x.Themes).HasColumnType("longtext").IsRequired();
            e.Property(x => x.Sentiment).HasMaxLength(20).IsRequired();
            e.Property(x => x.Entities).HasColumnType("longtext").IsRequired();
            e.Property(x => x.Summary).HasColumnType("text");
            e.Property(x => x.LanguageDetected).HasMaxLength(10);
            e.HasIndex(x => x.SerpResultId);
            e.HasOne(x => x.SerpResult).WithOne(s => s.AiAnalysis).HasForeignKey<AiAnalysis>(x => x.SerpResultId).OnDelete(DeleteBehavior.Cascade);
        });

        // --- AiVisibility ---
        modelBuilder.Entity<AiVisibility>(e =>
        {
            e.ToTable("ai_visibility");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.ScanId).HasMaxLength(36).IsFixedLength().IsRequired();
            e.Property(x => x.Keyword).HasMaxLength(500).IsRequired();
            e.Property(x => x.LlmModel).HasMaxLength(30).IsRequired();
            e.Property(x => x.ResponseText).HasColumnType("text").IsRequired();
            e.Property(x => x.CompetitorMentions).HasColumnType("longtext");
            e.Property(x => x.TopicsCovered).HasColumnType("longtext");
            e.HasIndex(x => new { x.ScanId, x.Keyword });
            e.HasIndex(x => x.LlmModel);
            e.HasOne(x => x.Scan).WithMany(s => s.AiVisibilities).HasForeignKey(x => x.ScanId).OnDelete(DeleteBehavior.Cascade);
        });

        // --- Tag ---
        modelBuilder.Entity<Tag>(e =>
        {
            e.ToTable("tags");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.ProjectId).HasMaxLength(36).IsFixedLength().IsRequired();
            e.Property(x => x.Name).HasMaxLength(255).IsRequired();
            e.Property(x => x.Slug).HasMaxLength(255).IsRequired();
            e.HasIndex(x => new { x.ProjectId, x.Slug }).IsUnique();
            e.HasIndex(x => new { x.ProjectId, x.Count });
            e.HasOne(x => x.Project).WithMany(p => p.Tags).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
        });

        // --- TagScan ---
        modelBuilder.Entity<TagScan>(e =>
        {
            e.ToTable("tag_scans");
            e.HasKey(x => new { x.TagId, x.ScanId });
            e.Property(x => x.TagId).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.ScanId).HasMaxLength(36).IsFixedLength();
            e.HasOne(x => x.Tag).WithMany(t => t.TagScans).HasForeignKey(x => x.TagId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Scan).WithMany(s => s.TagScans).HasForeignKey(x => x.ScanId).OnDelete(DeleteBehavior.Cascade);
        });

        // --- TagBlacklist ---
        modelBuilder.Entity<TagBlacklist>(e =>
        {
            e.ToTable("tag_blacklist");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.ProjectId).HasMaxLength(36).IsFixedLength().IsRequired();
            e.Property(x => x.TagName).HasMaxLength(255).IsRequired();
            e.HasIndex(x => new { x.ProjectId, x.TagName }).IsUnique();
            e.HasOne(x => x.Project).WithMany(p => p.TagBlacklists).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
        });

        // --- Metric ---
        modelBuilder.Entity<Metric>(e =>
        {
            e.ToTable("metrics");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.ProjectId).HasMaxLength(36).IsFixedLength().IsRequired();
            e.Property(x => x.ScanId).HasMaxLength(36).IsFixedLength().IsRequired();
            e.Property(x => x.MetricType).HasMaxLength(10).IsRequired();
            e.Property(x => x.Breakdown).HasColumnType("longtext").IsRequired();
            e.HasIndex(x => new { x.ProjectId, x.MetricType });
            e.HasOne(x => x.Project).WithMany(p => p.Metrics).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Scan).WithMany(s => s.Metrics).HasForeignKey(x => x.ScanId).OnDelete(DeleteBehavior.Cascade);
        });

        // --- Insight ---
        modelBuilder.Entity<Insight>(e =>
        {
            e.ToTable("insights");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.ProjectId).HasMaxLength(36).IsFixedLength().IsRequired();
            e.Property(x => x.ScanId).HasMaxLength(36).IsFixedLength().IsRequired();
            e.Property(x => x.Type).HasMaxLength(20).IsRequired();
            e.Property(x => x.Title).HasMaxLength(500).IsRequired();
            e.Property(x => x.Description).HasColumnType("text").IsRequired();
            e.Property(x => x.DataEvidence).HasColumnType("longtext").IsRequired();
            e.Property(x => x.Status).HasMaxLength(20).IsRequired();
            e.HasIndex(x => new { x.ProjectId, x.Score });
            e.HasOne(x => x.Project).WithMany(p => p.Insights).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Scan).WithMany(s => s.Insights).HasForeignKey(x => x.ScanId).OnDelete(DeleteBehavior.Cascade);
        });

        // --- EditorialPlan ---
        modelBuilder.Entity<EditorialPlan>(e =>
        {
            e.ToTable("editorial_plan");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.ProjectId).HasMaxLength(36).IsFixedLength().IsRequired();
            e.Property(x => x.InsightId).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.Title).HasMaxLength(500).IsRequired();
            e.Property(x => x.KeywordTarget).HasMaxLength(255);
            e.Property(x => x.Status).HasMaxLength(20).IsRequired();
            e.Property(x => x.AssignedTo).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.Notes).HasColumnType("text");
            e.HasIndex(x => new { x.ProjectId, x.Status });
            e.HasOne(x => x.Project).WithMany(p => p.EditorialPlans).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Insight).WithMany(i => i.EditorialPlans).HasForeignKey(x => x.InsightId).OnDelete(DeleteBehavior.SetNull);
        });

        // --- ContentBrief ---
        modelBuilder.Entity<ContentBrief>(e =>
        {
            e.ToTable("content_briefs");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.EditorialPlanId).HasMaxLength(36).IsFixedLength().IsRequired();
            e.Property(x => x.KeywordTarget).HasMaxLength(255).IsRequired();
            e.Property(x => x.SerpContext).HasColumnType("longtext").IsRequired();
            e.Property(x => x.AiContext).HasColumnType("longtext").IsRequired();
            e.Property(x => x.SuggestedStructure).HasColumnType("longtext").IsRequired();
            e.Property(x => x.Subtopics).HasColumnType("longtext").IsRequired();
            e.HasOne(x => x.EditorialPlan).WithMany(ep => ep.ContentBriefs).HasForeignKey(x => x.EditorialPlanId).OnDelete(DeleteBehavior.Cascade);
        });

        // --- ContentDraft ---
        modelBuilder.Entity<ContentDraft>(e =>
        {
            e.ToTable("content_drafts");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.ContentBriefId).HasMaxLength(36).IsFixedLength().IsRequired();
            e.Property(x => x.ContentHtml).HasColumnType("text").IsRequired();
            e.Property(x => x.VoiceCheckDetails).HasColumnType("longtext");
            e.Property(x => x.SeoCheck).HasColumnType("longtext");
            e.HasOne(x => x.ContentBrief).WithMany(cb => cb.ContentDrafts).HasForeignKey(x => x.ContentBriefId).OnDelete(DeleteBehavior.Cascade);
        });

        // --- JobQueue ---
        modelBuilder.Entity<JobQueue>(e =>
        {
            e.ToTable("job_queue");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasMaxLength(36).IsFixedLength();
            e.Property(x => x.ScanId).HasMaxLength(36).IsFixedLength().IsRequired();
            e.Property(x => x.JobType).HasMaxLength(30).IsRequired();
            e.Property(x => x.Keyword).HasMaxLength(500).IsRequired();
            e.Property(x => x.Status).HasMaxLength(20).IsRequired();
            e.Property(x => x.ErrorMessage).HasColumnType("text");
            e.HasIndex(x => new { x.Status, x.CreatedAt });
            e.HasOne(x => x.Scan).WithMany(s => s.Jobs).HasForeignKey(x => x.ScanId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
