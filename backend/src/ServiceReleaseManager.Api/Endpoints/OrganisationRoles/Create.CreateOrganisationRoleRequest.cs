using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoles;

public class CreateOrganisationRoleRequest
{
  [Required]
  public int OrganisationId { get; set; } = default!;

  [Required]
  [MinLength(5)]
  [MaxLength(50)]
  public string Name { get; set; } = default!;

  [Required]
  public bool ServiceWrite { get; set; } = default!;

  [Required]
  public bool ServiceDelete { get; set; } = default!;

  [Required]
  public bool UserRead { get; set; } = default!;

  [Required]
  public bool UserWrite { get; set; } = default!;

  [Required]
  public bool UserDelete { get; set; } = default!;
}
