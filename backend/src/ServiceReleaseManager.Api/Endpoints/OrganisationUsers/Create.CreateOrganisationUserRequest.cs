using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUsers;

public class CreateOrganisationUserRequest
{
  [Required]
  public int OrganisationId { get; set; } = default!;

  [Required]
  [EmailAddress]
  public string Email { get; set; } = default!;

  [Required]
  [MinLength(5)]
  [MaxLength(50)]
  public string FirstName { get; set; } = default!;

  [Required]
  [MinLength(5)]
  [MaxLength(50)]
  public string LastName { get; set; } = default!;
  
  [Required]
  public int RoleId { get; set; } = default!;
}
