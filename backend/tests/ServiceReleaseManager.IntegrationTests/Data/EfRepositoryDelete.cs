using ServiceReleaseManager.Core.OrganisationAggregate;
using Xunit;

namespace ServiceReleaseManager.IntegrationTests.Data;

public class EfRepositoryDelete : BaseEfRepoTestFixture
{
  [Fact]
  public async Task DeletesItemAfterAddingIt()
  {
    // add a project
    var repository = GetRepository();
    var initialName = Guid.NewGuid().ToString();
    var organisation = new Organisation(initialName);
    await repository.AddAsync(organisation);

    // delete the item
    await repository.DeleteAsync(organisation);

    // verify it's no longer there
    Assert.DoesNotContain(await repository.ListAsync(),
      project => project.Name == initialName);
  }
}
