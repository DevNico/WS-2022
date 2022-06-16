using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class CreateOrganisationRequest
{
  [Required]
  public string Name { get; set; } = default!;
}
