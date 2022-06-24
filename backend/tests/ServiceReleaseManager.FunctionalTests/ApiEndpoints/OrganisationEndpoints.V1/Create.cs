using System.Net;
using System.Net.Http.Json;
using IdentityModel.Client;
using ServiceReleaseManager.Api;
using ServiceReleaseManager.Api.Endpoints.Organisations;
using Xunit;

namespace ServiceReleaseManager.FunctionalTests.ApiEndpoints.OrganisationEndpoints.V1;

[Collection("Sequential")]
public class Create : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private const string Route = "/api/v1/organisations";
  private readonly CustomWebApplicationFactory<WebMarker> _factory;

  public Create(CustomWebApplicationFactory<WebMarker> factory)
  {
    _factory = factory;
  }

  [Fact]
  public async Task CreateOrganisationGivenAdminUser()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetAdminUserToken());

    var result = await client.PostAsync(
      Route,
      JsonContent.Create(new CreateOrganisationRequest { Name = "Test Name" })
    );
    var record = await result.Content.ReadFromJsonAsync<OrganisationRecord>();

    Assert.NotNull(record);
    Assert.Equal(3, record?.Id);
    Assert.Equal("Test Name", record?.Name);
    Assert.Equal("test-name", record?.RouteName);
  }

  [Fact]
  public async Task ForbiddenGivenNormalUser()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetNormalUserToken());

    var result = await client.PostAsync(
      Route,
      JsonContent.Create(new CreateOrganisationRequest { Name = "Test Name" })
    );

    Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
  }
}
