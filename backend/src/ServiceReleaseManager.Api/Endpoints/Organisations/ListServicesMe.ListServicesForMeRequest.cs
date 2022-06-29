using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListServicesForMeRequest
{
  public const string Route = "{OrganisationRouteName}/services/me";

  [Required]
  [FromRoute]
  public string OrganisationRouteName { get; set; }
}
