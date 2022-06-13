using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Infrastructure.Data;

public class AppDbContext : DbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;

  public AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher? dispatcher)
    : base(options)
  {
    _dispatcher = dispatcher;
  }

  public DbSet<Organisation> Organisations => Set<Organisation>();
  public DbSet<Service> Services => Set<Service>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
  {
    AddTimestamps();

    var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_dispatcher == null)
    {
      return result;
    }

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
      .Select(e => e.Entity)
      .Where(e => e.DomainEvents.Any())
      .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }

  private void AddTimestamps()
  {
    var entities = ChangeTracker.Entries()
      .Where(x => x.Entity is EntityBase && x.State is EntityState.Added or EntityState.Modified);

    foreach (var entity in entities)
    {
      var now = DateTime.UtcNow;

      if (entity.State == EntityState.Added)
      {
        ((EntityBase)entity.Entity).CreatedAt = now;
      }

      ((EntityBase)entity.Entity).UpdatedAt = now;
    }
  }
}
