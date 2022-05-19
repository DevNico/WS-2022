using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ServiceReleaseManager.Domain.Models.User;

namespace ServiceReleaseManager.DataAccess;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<User> Users { get; init; }
    public DbSet<UserOrganisation> UserOrganisations { get; init; }
    public DbSet<UserService> UserServices { get; init; }
}
