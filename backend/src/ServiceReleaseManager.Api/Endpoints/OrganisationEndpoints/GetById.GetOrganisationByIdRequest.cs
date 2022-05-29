namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class GetOrganisationByIdRequest
{
  public const string Route = "/organisations/{OrganisationId:int}";

  public int OrganisationId { get; set; }

  public static string BuildRoute(int organisationId)
  {
    return Route.Replace("{OrganisationId:int}", organisationId.ToString());
  }
}
