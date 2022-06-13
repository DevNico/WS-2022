using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class DeleteOrganisationRoleRequest
{
  public const string Route = "organisation-roles/{OrganisationRoleId}";

  [Required]
  public int OrganisationRoleId { get; set; } = default!;

  static string BuildRoute(int organisationRoleId)
  {
    return Route.Replace("{OrganisationRoleId}", organisationRoleId.ToString());
  }
}
