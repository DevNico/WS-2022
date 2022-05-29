using System.Net;
using System.Net.Http.Headers;
using Ardalis.HttpClientTestExtensions;
using IdentityModel.Client;
using ServiceReleaseManager.Api;
using ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;
using Xunit;

namespace ServiceReleaseManager.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class ProjectGetById : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly CustomWebApplicationFactory<WebMarker> _factory;

  public ProjectGetById(CustomWebApplicationFactory<WebMarker> factory)
  {
    _factory = factory;
  }

  [Fact]
  public async Task ReturnsSeedProjectGivenId1WhenAuthorized()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetAdminUserToken());

    var route = GetOrganisationByIdRequest.BuildRoute(1);
    var result = await client.GetAndDeserialize<OrganisationRecord>(route);

    Assert.Equal(1, result.Id);
    Assert.Equal(SeedData.TestOrganisation1.Name, result.Name);
  }

  [Fact]
  public async Task ReturnsNotFoundGivenNormalUser()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetNormalUserToken());

    var route = GetOrganisationByIdRequest.BuildRoute(1);
    await client.GetAndEnsureNotFound(route);
  }

  [Fact]
  public async Task ReturnsNotFoundGivenId0AndAuthorized()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetAdminUserToken());

    var route = GetOrganisationByIdRequest.BuildRoute(0);

    await client.GetAndEnsureNotFound(route);
  }
}
