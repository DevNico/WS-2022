using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class ListOrganisationUserRequest
{
  public const string Route = "/organisations/{OrganisationRouteName}/users";

  [Required]
  [FromRoute]
  public string OrganisationRouteName { get; set; } = default!;

  public static string BuildRoute(string organisationRouteName)
  {
    return Route.Replace("{OrganisationRouteName}", organisationRouteName);
  }
}
