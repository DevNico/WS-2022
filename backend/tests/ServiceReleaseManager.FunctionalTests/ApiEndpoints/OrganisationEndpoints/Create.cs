using System.Net;
using System.Net.Http.Json;
using IdentityModel.Client;
using ServiceReleaseManager.Api;
using ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;
using ServiceReleaseManager.Core.OrganisationAggregate;
using Xunit;

namespace ServiceReleaseManager.FunctionalTests.ApiEndpoints.OrganisationEndpoints;

[Collection("Sequential")]
public class Create : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  public Create(CustomWebApplicationFactory<WebMarker> factory)
  {
    _factory = factory;
  }

  private readonly CustomWebApplicationFactory<WebMarker> _factory;

  [Fact]
  public async Task CreateOrganisationGivenAdminUser()
  {
    var client = _factory.CreateClient();
    client.SetBearerToken(ApiTokenHelper.GetAdminUserToken());

    var result = await client.PostAsync(
      CreateOrganisationRequest.Route,
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
      CreateOrganisationRequest.Route,
      JsonContent.Create(new CreateOrganisationRequest { Name = "Test Name" })
    );

    Assert.Equal(HttpStatusCode.Forbidden, result.StatusCode);
  }
}
