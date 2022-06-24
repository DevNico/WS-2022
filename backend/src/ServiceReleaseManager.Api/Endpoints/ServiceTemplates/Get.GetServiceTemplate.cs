using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class GetServiceTemplate
{
  public const string Route = "{ServiceTemplateName}";

  [Required]
  public string ServiceTemplateName { get; set; } = default!;
}
