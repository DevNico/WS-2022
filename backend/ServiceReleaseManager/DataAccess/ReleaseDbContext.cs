using Microsoft.EntityFrameworkCore;
using ServiceReleaseManager.Domain.Models.Release;

namespace ServiceReleaseManager.DataAccess;

public class ReleaseDbContext : DbContext
{
    public ReleaseDbContext(DbContextOptions<ReleaseDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Release> Releases { get; set; }
    public DbSet<ReleaseLocalizedMetadata> ReleaseLocalizedMetadatas { get; set; }
    public DbSet<ReleaseTarget> ReleaseTargets { get; set; }
    public DbSet<ReleaseTrigger> ReleaseTriggers { get; set; }
}