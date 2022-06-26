using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class GetOrganisationByIdRequest
{
  public const string Route = "{OrganisationId:int}";

  [FromRoute]
  public int OrganisationId { get; set; } = default!;
}
