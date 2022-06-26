using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class UpdateServiceTemplate
{
  [FromRoute]
  public int ServiceTemplateId { get; set; } = default!;

  [Required]
  public string Name { get; set; } = default!;

  [Required]
  public List<MetadataArrayElement> StaticMetadata { get; set; } = default!;

  [Required]
  public List<MetadataArrayElement> LocalizedMetadata { get; set; } = default!;
}
