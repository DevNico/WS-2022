namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class GetOrganisationUserByUserIdRequest
{
  public const string Route = "/organisationusers/{UserId:string}";

  public string? UserId { get; set; }

  public static string BuildRoute(string userId)
  {
    return Route.Replace("{UserId:string}", userId);
  }
}
