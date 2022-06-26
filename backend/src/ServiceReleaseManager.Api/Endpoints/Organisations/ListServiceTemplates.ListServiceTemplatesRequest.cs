using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListServiceTemplatesRequest
{
  public const string Route = "{OrganisationRouteName}/services-templates";

  [Required]
  [FromRoute]
  public string OrganisationRouteName { get; set; } = default!;
}
