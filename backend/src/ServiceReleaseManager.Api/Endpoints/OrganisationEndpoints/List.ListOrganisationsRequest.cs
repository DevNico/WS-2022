using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class ListOrganisationsRequest
{
  public const string Route = "/organisations";

  [FromQuery(Name = "includeDeactivated")]
  public bool IncludeDeactivated { get; set; } = default!;

  public static string BuildRoute(bool includeDeactivated) =>
    $"{Route}?includeDeactivated={includeDeactivated}";
}
