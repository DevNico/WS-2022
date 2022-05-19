using Microsoft.EntityFrameworkCore;
using ServiceReleaseManager.Domain.Models.Service;

namespace ServiceReleaseManager.DataAccess;

public class ServiceDbContext : DbContext
{
    public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Service> Services { get; init; }
    public DbSet<ServiceRole> ServiceRoles { get; init; }
    public DbSet<ServiceTemplate> ServiceTemplates { get; init; }
}