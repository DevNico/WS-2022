using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUsers;

public class DeleteOrganisationUserRequest
{
  public const string Route = "{OrganisationUserId:int}";

  [Required]
  public int OrganisationUserId { get; set; } = default!;
}
