using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListOrganisationServicesRequest
{
  public const string Route = "{OrganisationRouteName}/services";

  [Required]
  [FromRoute]
  public string OrganisationRouteName { get; set; } = default!;
}
