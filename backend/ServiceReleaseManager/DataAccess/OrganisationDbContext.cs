using Microsoft.EntityFrameworkCore;
using ServiceReleaseManager.Domain.Models.Organisation;

namespace ServiceReleaseManager.DataAccess;

public class OrganisationDbContext : DbContext
{
    public OrganisationDbContext(DbContextOptions<OrganisationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Organisation> Organisations { get; set; }
    public DbSet<OrganisationRole> OrganisationRoles { get; set; }
}