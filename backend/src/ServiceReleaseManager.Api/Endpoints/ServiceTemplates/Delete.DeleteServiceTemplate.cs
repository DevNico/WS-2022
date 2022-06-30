using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class DeleteServiceTemplate
{
  public const string Route = "{ServiceTemplateId:int}";

  [Required]
  [FromRoute]
  public int ServiceTemplateId { get; set; } = default!;
}
