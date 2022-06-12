using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.Api.Routes;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class CreateServiceTemplate
{
  public const string Route = RouteHelper.BaseRoute;
  
  [Required] public string? Name { get; set; }
  [Required] public string? StaticMetadata { get; set; }
  [Required] public string? LocalizedMetadata { get; set; }
}
