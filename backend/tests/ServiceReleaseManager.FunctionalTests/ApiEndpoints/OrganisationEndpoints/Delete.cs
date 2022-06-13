using System.Net;
using Ardalis.HttpClientTestExtensions;
using IdentityModel.Client;
using ServiceReleaseManager.Api;
using ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;
using Xunit;

namespace ServiceReleaseManager.FunctionalTests.ApiEndpoints.OrganisationEndpoints;

[Collection("Sequential")]
public class Delete : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly CustomWebApplicationFactory<WebMarker> _factory;

  public Delete(CustomWebApplicationFactory<WebMarker> factory)
  {
    _factory = factory;
  }

  [Fact]
  public async Task DeleteOrganisationGivenAdminUser()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetAdminUserToken());

    var result =
      await client.DeleteAsync(
        DeleteOrganisationRequest.BuildRoute(SeedData.TestOrganisation1.RouteName));

    Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
  }

  [Fact]
  public async Task ForbiddenGivenNormalUser()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetNormalUserToken());

    var result= await client.DeleteAsync(
      DeleteOrganisationRequest.BuildRoute(SeedData.TestOrganisation1.RouteName));
    
    Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
  }
}
