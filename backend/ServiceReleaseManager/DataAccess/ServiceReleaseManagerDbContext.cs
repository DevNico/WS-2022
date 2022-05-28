using Microsoft.EntityFrameworkCore;
using ServiceReleaseManager.Domain.Models.Organisation;
using ServiceReleaseManager.Domain.Models.Release;
using ServiceReleaseManager.Domain.Models.Service;
using ServiceReleaseManager.Domain.Models.User;

namespace ServiceReleaseManager.DataAccess;

public class ServiceReleaseManagerDbContext : DbContext
{
    public ServiceReleaseManagerDbContext(DbContextOptions<ServiceReleaseManagerDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; init; }
    public DbSet<UserOrganisation> UserOrganisations { get; init; }
    public DbSet<UserService> UserServices { get; init; }
    
    public DbSet<Service> Services { get; init; }
    public DbSet<ServiceRole> ServiceRoles { get; init; }
    public DbSet<ServiceTemplate> ServiceTemplates { get; init; }
    
    public DbSet<Release> Releases { get; set; }
    public DbSet<ReleaseLocalizedMetadata> ReleaseLocalizedMetadatas { get; set; }
    public DbSet<ReleaseTarget> ReleaseTargets { get; set; }
    public DbSet<ReleaseTrigger> ReleaseTriggers { get; set; }
    
    public DbSet<Organisation> Organisations { get; set; }
    public DbSet<OrganisationRole> OrganisationRoles { get; set; }
}