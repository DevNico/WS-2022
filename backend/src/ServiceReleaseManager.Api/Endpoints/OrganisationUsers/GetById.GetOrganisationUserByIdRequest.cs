using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUsers;

public class GetOrganisationUserByIdRequest
{
  public const string Route = "{OrganisationUserId:int}";

  [Required]
  public int OrganisationUserId { get; set; } = default!;
}
