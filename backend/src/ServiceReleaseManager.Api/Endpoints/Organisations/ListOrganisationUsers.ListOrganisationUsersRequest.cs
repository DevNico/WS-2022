using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListOrganisationUsersRequest
{
  public const string Route = "{OrganisationRouteName}/users";

  [Required]
  [FromRoute]
  public string OrganisationRouteName { get; set; } = default!;
}
