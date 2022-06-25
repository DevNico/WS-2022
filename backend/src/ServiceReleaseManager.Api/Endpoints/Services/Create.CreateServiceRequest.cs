using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class CreateServiceRequest
{
  [Required]
  [MinLength(2)]
  [MaxLength(50)]
  public string Name { get; set; }

  [Required]
  public string Description { get; set; }

  [Required]
  public int OrganisationId { get; set; }
}
