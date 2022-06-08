using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class CreateOrganisationRoleRequest
{
  public const string Route = "/organisationroles";

  [Required] [MaxLength(50)] public string? Name { get; set; }

  [Required] public bool ServiceRead { get; set; }

  [Required] public bool ServiceWrite { get; set; }

  [Required] public bool ServiceDelete { get; set; }

  [Required] public bool UserRead { get; set; }

  [Required] public bool UserWrite { get; set; }

  [Required] public bool UserDelete { get; set; }

}
