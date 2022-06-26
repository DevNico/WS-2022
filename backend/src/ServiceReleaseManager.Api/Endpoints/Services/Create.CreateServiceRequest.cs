using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class CreateServiceRequest
{
  [Required, MinLength(2), MaxLength(50)]
  public string Name { get; set; } = default!;

  [Required]
  public string Description { get; set; } = default!;

  [Required]
  public int ServiceTemplateId { get; set; } = default!;
}
