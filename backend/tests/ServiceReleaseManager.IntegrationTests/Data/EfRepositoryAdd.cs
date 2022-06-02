using ServiceReleaseManager.Core.OrganisationAggregate;
using Xunit;

namespace ServiceReleaseManager.IntegrationTests.Data;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
  [Fact]
  public async Task AddsProjectAndSetsId()
  {
    const string testOrganisationName = "testOrganisation";
    var repository = GetRepository();
    var organisation = new Organisation(testOrganisationName);

    await repository.AddAsync(organisation);

    var newOrganisation = (await repository.ListAsync()).FirstOrDefault();

    Assert.Equal(testOrganisationName, newOrganisation?.Name);
    Assert.Equal(true, newOrganisation?.IsActive);
    Assert.True(newOrganisation?.Id > 0);
  }
}
