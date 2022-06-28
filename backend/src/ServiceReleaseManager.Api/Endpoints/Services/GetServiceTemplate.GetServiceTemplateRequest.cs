using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class ListServiceTemplateRequest
{
  public const string Route = "{ServiceRouteName}/services-template";

  [Required]
  [FromRoute]
  public string ServiceRouteName { get; set; } = default!;
}
