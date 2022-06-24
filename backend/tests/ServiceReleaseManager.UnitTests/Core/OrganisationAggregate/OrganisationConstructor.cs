using ServiceReleaseManager.Core.OrganisationAggregate;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Core.OrganisationAggregate;

public class OrganisationConstructor
{
  private readonly string _testName = "test name";
  private readonly string _testRouteName = "test-name";
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
  public void InitializesRouteName()
  {
    _testOrganisation = CreateOrganisation();

    Assert.Equal(_testRouteName, _testOrganisation.RouteName);
  }

  [Fact]
  public void InitializesIsActive()
  {
    _testOrganisation = CreateOrganisation();

    Assert.True(_testOrganisation.IsActive);
  }

  [Fact]
  public void InitializesRoles()
  {
    _testOrganisation = CreateOrganisation();

    Assert.Empty(_testOrganisation.Roles);
  }

  [Fact]
  public void InitializesUsesr()
  {
    _testOrganisation = CreateOrganisation();

    Assert.Empty(_testOrganisation.Users);
  }

  [Fact]
  public void InitializesServices()
  {
    _testOrganisation = CreateOrganisation();

    Assert.Empty(_testOrganisation.Services);
  }
}
