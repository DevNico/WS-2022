using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.Core.Interfaces;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class CreateServiceTemplate
{
  [Required]
  public string? Name { get; set; } = default!;

  [Required]
  public List<MetadataArrayElement>? StaticMetadata { get; set; } = default!;

  [Required]
  public List<MetadataArrayElement>? LocalizedMetadata { get; set; } = default!;
}
