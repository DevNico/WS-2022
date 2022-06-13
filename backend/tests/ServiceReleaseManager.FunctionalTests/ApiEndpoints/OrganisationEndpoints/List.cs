using System.Net;
using Ardalis.HttpClientTestExtensions;
using IdentityModel.Client;
using ServiceReleaseManager.Api;
using ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;
using Xunit;

namespace ServiceReleaseManager.FunctionalTests.ApiEndpoints.OrganisationEndpoints;

[Collection("Sequential")]
public class List : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly CustomWebApplicationFactory<WebMarker> _factory;

  public List(CustomWebApplicationFactory<WebMarker> factory)
  {
    _factory = factory;
  }

  [Fact]
  public async Task ReturnsOneOrganisationGivenAdminUser()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetAdminUserToken());

    var result =
      await client.GetAndDeserialize<List<OrganisationRecord>>(ListOrganisationsRequest.Route);

    Assert.Single(result);
    Assert.Contains(result, i => i.Name == SeedData.TestOrganisation1.Name);
  }

  [Fact]
  public async Task ReturnsOneOrganisationGivenAdminUserAndIncludeDeactivated()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetAdminUserToken());

    var result =
      await client.GetAndDeserialize<List<OrganisationRecord>>(ListOrganisationsRequest.Route);

    Assert.Single(result);
    Assert.Contains(result, i => i.Name == SeedData.TestOrganisation1.Name);
  }

  [Fact]
  public async Task ReturnsForbiddenGivenNormalUser()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetNormalUserToken());

    var result =
      await client.GetAsync(ListOrganisationsRequest.Route);

    Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
  }
}
