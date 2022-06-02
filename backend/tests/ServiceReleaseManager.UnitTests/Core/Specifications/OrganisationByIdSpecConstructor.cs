using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Core.Specifications;

public class OrganisationByIdSpecConstructor
{
  private static readonly Organisation _active =
    new("testOrganisation") { Id = 1 };

  private static readonly Organisation _inactive =
    new("testOrganisation") { Id = 2, IsActive = false };

  private readonly List<Organisation> _organisations = new() { _active, _inactive };

  [Fact]
  public void FilterCollectionToOnlyReturnOrganisationByIdWhenIsActiveTrue()
  {
    var spec = new OrganisationByIdSpec(_active.Id);
    var result = spec.Evaluate(_organisations).FirstOrDefault();

    Assert.NotNull(result);
    Assert.Equal(_active, result);
  }

  [Fact]
  public void FilterCollectionToOnlyReturnOrganisationByIdWhenIsActiveFalse()
  {
    var spec = new OrganisationByIdSpec(_inactive.Id);
    var result = spec.Evaluate(_organisations).FirstOrDefault();

    Assert.Null(result);
  }
}
