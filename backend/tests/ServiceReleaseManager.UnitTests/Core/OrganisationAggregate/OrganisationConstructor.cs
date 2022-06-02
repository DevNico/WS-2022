using ServiceReleaseManager.Core.OrganisationAggregate;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Core.OrganisationAggregate;

public class OrganisationConstructor
{
  private readonly string _testName = "test name";
  private Organisation? _testOrganisation;

  private Organisation CreateOrganisation()
  {
    return new Organisation(_testName);
  }

  [Fact]
  public void InitializesName()
  {
    _testOrganisation = CreateOrganisation();

    Assert.Equal(_testName, _testOrganisation.Name);
  }

  [Fact]
  public void InitializesIsActive()
  {
    _testOrganisation = CreateOrganisation();

    Assert.True(_testOrganisation.IsActive);
  }
}
