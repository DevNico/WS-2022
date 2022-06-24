using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoles;

public class DeleteOrganisationRoleRequest
{
  public const string Route = "{OrganisationRoleId:int}";

  [Required]
  public int OrganisationRoleId { get; set; } = default!;
}
