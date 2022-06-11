using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplateEndpoints;

public class CreateServiceTemplate
{
  public const string Route = "/servicetemplates";
  
  [Required] public string? Name { get; set; }
  [Required] public string? StaticMetadata { get; set; }
  [Required] public string? LocalizedMetadata { get; set; }
}
