using Ardalis.HttpClientTestExtensions;
using IdentityModel.Client;
using ServiceReleaseManager.Api;
using ServiceReleaseManager.Api.Endpoints.Organisations;
using Xunit;

namespace ServiceReleaseManager.FunctionalTests.ApiEndpoints.OrganisationEndpoints.V1;

[Collection("Sequential")]
public class GetByRouteName : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly CustomWebApplicationFactory<WebMarker> _factory;

  public GetByRouteName(CustomWebApplicationFactory<WebMarker> factory)
  {
    _factory = factory;
  }

  private static string _buildRoute(string routeName)
  {
    return $"/api/v1/organisations/${routeName}";
  }

  [Fact]
  public async Task ReturnsSeedOrganisationGivenId1WhenAuthorized()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetAdminUserToken());

    var route = _buildRoute(SeedData
      .TestOrganisation1
      .RouteName);
    var result = await client.GetAndDeserialize<OrganisationRecord>(route);

    Assert.Equal(1, result.Id);
    Assert.Equal(SeedData.TestOrganisation1.Name, result.Name);
  }

  [Fact]
  public async Task ReturnsNotFoundGivenNormalUser()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetNormalUserToken());

    var route = _buildRoute("not-found");
    await client.GetAndEnsureNotFound(route);
  }

  [Fact(Skip = "Authorization not implemented")]
  public async Task ReturnsNotFoundGivenSeedOrganisationRouteNameAndAuthorized()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetNormalUserToken());

    var route = _buildRoute(SeedData
      .TestOrganisation1.RouteName);

    await client.GetAndEnsureNotFound(route);
  }
}
