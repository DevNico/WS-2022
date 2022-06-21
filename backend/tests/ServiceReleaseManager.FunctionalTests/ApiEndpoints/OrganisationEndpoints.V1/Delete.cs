using System.Net;
using IdentityModel.Client;
using ServiceReleaseManager.Api;
using Xunit;

namespace ServiceReleaseManager.FunctionalTests.ApiEndpoints.OrganisationEndpoints.V1;

[Collection("Sequential")]
public class Delete : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly CustomWebApplicationFactory<WebMarker> _factory;

  public Delete(CustomWebApplicationFactory<WebMarker> factory)
  {
    _factory = factory;
  }

  private static string _buildRoute(string routeName)
  {
    return $"/api/v1/organisations/${routeName}";
  }

  [Fact]
  public async Task DeleteOrganisationGivenAdminUser()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetAdminUserToken());

    var result =
      await client.DeleteAsync(
        _buildRoute(SeedData.TestOrganisation1.RouteName));

    Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
  }

  [Fact]
  public async Task ForbiddenGivenNormalUser()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetNormalUserToken());

    var result = await client.DeleteAsync(
      _buildRoute(SeedData.TestOrganisation1.RouteName));

    Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
  }
}
