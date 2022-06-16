using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListOrganisationsRequest
{
  [FromQuery(Name = "includeDeactivated")]
  public bool IncludeDeactivated { get; set; } = default!;
}
