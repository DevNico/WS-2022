using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Infrastructure.Data;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.IntegrationTests.Data;

public abstract class BaseEfRepoTestFixture
{
  protected readonly AppDbContext _dbContext;

  protected BaseEfRepoTestFixture()
  {
    var options = CreateNewContextOptions();
    var mockEventDispatcher = new Mock<IDomainEventDispatcher?>();

    _dbContext = new AppDbContext(options, mockEventDispatcher.Object);
  }

  private static DbContextOptions<AppDbContext> CreateNewContextOptions()
  {
    // Create a fresh service provider, and therefore a fresh
    // InMemory database instance.
    var serviceProvider = new ServiceCollection()
                         .AddEntityFrameworkInMemoryDatabase()
                         .BuildServiceProvider();

    // Create a new options instance telling the context to use an
    // InMemory database and the new service provider.
    var builder = new DbContextOptionsBuilder<AppDbContext>();
    builder.UseInMemoryDatabase("service_release_manager")
           .UseInternalServiceProvider(serviceProvider);

    return builder.Options;
  }

  protected EfRepository<Organisation> GetRepository()
  {
    return new EfRepository<Organisation>(_dbContext);
  }

  protected EfRepository<OrganisationRole> GetOrganisationRoleRepository()
  {
    return new EfRepository<OrganisationRole>(_dbContext);
  }

  protected EfRepository<OrganisationUser> GetOrganisationUserRepository()
  {
    return new EfRepository<OrganisationUser>(_dbContext);
  }

  protected EfRepository<Service> GetServiceRepository()
  {
    return new EfRepository<Service>(_dbContext);
  }

  protected EfRepository<Release> GetReleaseRepository()
  {
    return new EfRepository<Release>(_dbContext);
  }
}
