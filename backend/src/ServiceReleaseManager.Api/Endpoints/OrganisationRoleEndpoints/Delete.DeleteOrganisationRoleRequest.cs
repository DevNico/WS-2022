using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class DeleteOrganisationRoleRequest
{
  [Required] public string? OrganisationName { get; set; }
  [Required] public int RoleId { get; set; }
}
