using Microsoft.EntityFrameworkCore;
using ServiceReleaseManager.Core.OrganisationAggregate;
using Xunit;

namespace ServiceReleaseManager.IntegrationTests.Data;

public class EfRepositoryUpdate : BaseEfRepoTestFixture
{
  [Fact]
  public async Task UpdatesItemAfterAddingIt()
  {
    // add a organisation
    var repository = GetRepository();
    var organisation = new Organisation(Guid.NewGuid().ToString());
    await repository.AddAsync(organisation);

    // detach the item so we get a different instance
    _dbContext.Entry(organisation).State = EntityState.Detached;

    // fetch the item and update its title
    var newOrganisation = (await repository.ListAsync())
      .FirstOrDefault(project => project.Id == organisation.Id);
    if (newOrganisation == null)
    {
      Assert.NotNull(newOrganisation);
      return;
    }

    Assert.NotSame(organisation, newOrganisation);

    // Update the item
    newOrganisation.Deactivate();
    await repository.UpdateAsync(newOrganisation);

    // Fetch the updated item
    var updatedItem = (await repository.ListAsync())
      .FirstOrDefault(project => project.Id == newOrganisation.Id);

    Assert.NotNull(updatedItem);
    Assert.Equal(organisation.Name, updatedItem?.Name);
    Assert.NotEqual(organisation.IsActive, updatedItem?.IsActive);
    Assert.Equal(newOrganisation.Id, updatedItem?.Id);
  }
}
