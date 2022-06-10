using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class CreateOrganisationRequest
{
  [Required] public string? Name { get; set; }
}
