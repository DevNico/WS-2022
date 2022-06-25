using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListServiceRolesRequest
{
  public const string Route = "{OrganisationRouteName}/service-roles";

  [Required]
  [FromRoute]
  public string OrganisationRouteName { get; set; } = default!;
}
