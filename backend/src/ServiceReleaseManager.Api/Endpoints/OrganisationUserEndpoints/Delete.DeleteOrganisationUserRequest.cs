using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class DeleteOrganisationUserRequest
{
  public const string Route = "users/{OrganisationUserId}";

  [Required]
  public int OrganisationUserId { get; set; } = default!;

  public static string BuildRoute(int organisationUserId)
  {
    return Route.Replace("{OrganisationUserId}", organisationUserId.ToString());
  }
}
