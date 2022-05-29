using Ardalis.HttpClientTestExtensions;
using ServiceReleaseManager.Api;
using ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;
using Xunit;

namespace ServiceReleaseManager.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class ProjectList : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public ProjectList(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnsOneProject()
  {
    var result = await _client.GetAndDeserialize<List<OrganisationRecord>>("/organisations");

    Assert.Single(result);
    Assert.Contains(result, i => i.Name == SeedData.TestOrganisation1.Name);
  }
}
