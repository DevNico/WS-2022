using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceUsers;

public class CreateServiceUserRequest
{
  [Required]
  public int ServiceId { get; set; }

  [Required]
  public int OrganisationUserId { get; set; }

  [Required]
  public int ServiceRoleId { get; set; }
}
