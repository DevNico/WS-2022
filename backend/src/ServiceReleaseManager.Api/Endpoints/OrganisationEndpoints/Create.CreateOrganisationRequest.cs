using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class CreateOrganisationRequest
{
  public const string Route = "/organisations";

  [Required] public string? Name { get; set; }
}
