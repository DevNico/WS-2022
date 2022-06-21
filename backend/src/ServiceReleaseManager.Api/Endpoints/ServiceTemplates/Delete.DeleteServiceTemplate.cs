using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class DeleteServiceTemplate
{
  public const string Route = "{ServiceTemplateId:int}";

  [Required]
  public int ServiceTemplateId { get; set; } = default!;
}
