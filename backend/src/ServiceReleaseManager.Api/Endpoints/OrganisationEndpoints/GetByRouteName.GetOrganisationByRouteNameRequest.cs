using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class GetOrganisationByRouteNameRequest
{
  public const string Route = "/organisations/{OrganisationRouteName}";

  [FromRoute]
  public string OrganisationRouteName { get; set; } = default!;

  public static string BuildRoute(string organisationRouteName) =>
    Route.Replace("{OrganisationRouteName}", organisationRouteName);
}
