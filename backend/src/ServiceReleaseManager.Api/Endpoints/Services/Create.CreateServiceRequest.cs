using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class CreateServiceRequest
{
  [Required] public string? Name { get; set; }
  [Required] public string? Description { get; set; }
  [Required] public int OrganisationId { get; set; }
}
