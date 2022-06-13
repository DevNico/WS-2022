using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class ListOrganisationRoleRequest
{
  [Required] public string? OrganisationName { get; set; }
}
