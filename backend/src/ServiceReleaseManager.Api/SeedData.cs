using Microsoft.EntityFrameworkCore;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Infrastructure.Data;

namespace ServiceReleaseManager.Api;

public static class SeedData
{
  public static readonly Organisation TestOrganisation1 =
    new("Test Organisation");

  public static readonly Organisation TestOrganisation2 =
    new("Test Organisation Deactivated") { IsActive = false };

  public static readonly Service TestService1 =
    new("Test Service", "For testing purposes");

  public static void Initialize(IServiceProvider serviceProvider)
  {
    using var dbContext = new AppDbContext(
      serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null);
    if (dbContext.Organisations.Any())
    {
      return; // DB has been seeded
    }

    PopulateTestData(dbContext);
  }

  public static void PopulateTestData(AppDbContext dbContext)
  {
    foreach (var item in dbContext.Organisations)
    {
      dbContext.Remove(item);
    }

    dbContext.SaveChanges();

    TestOrganisation1.Services.Add(TestService1);
    dbContext.Organisations.Add(TestOrganisation1);
    dbContext.Organisations.Add(TestOrganisation2);

    dbContext.SaveChanges();
  }
}
