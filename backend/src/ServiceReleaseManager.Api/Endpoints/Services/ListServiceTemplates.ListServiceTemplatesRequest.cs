using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class ListServiceTemplatesRequest
{
  public const string Route = "{ServiceRouteName}/services-templates";

  [Required]
  [FromRoute]
  public string ServiceRouteName { get; set; } = default!;
}
