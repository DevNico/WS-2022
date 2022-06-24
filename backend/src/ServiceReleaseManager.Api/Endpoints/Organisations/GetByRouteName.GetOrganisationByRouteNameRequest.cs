using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class GetOrganisationByRouteNameRequest
{
  public const string Route = "{OrganisationRouteName}";

  [FromRoute]
  public string OrganisationRouteName { get; set; } = default!;
}
