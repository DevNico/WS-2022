using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.Api.Routes;
using ServiceReleaseManager.Core.Interfaces;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class UpdateServiceTemplate
{
  public const string Route = RouteHelper.BaseRoute;

  [Required] public string? Name { get; set; }
  [Required] public List<MetadataArrayElement>? StaticMetadata { get; set; }
  [Required] public List<MetadataArrayElement>? LocalizedMetadata { get; set; }
}
