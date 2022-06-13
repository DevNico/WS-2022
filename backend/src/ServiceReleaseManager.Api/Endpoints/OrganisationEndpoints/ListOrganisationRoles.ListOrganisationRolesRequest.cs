namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class ListOrganisationRolesRequest
{
  public const string Route = "/organisation/{OrganisationRouteName}/roles";

  public string OrganisationRouteName { get; set; } = default!;

  public static string BuildRoute(string organisationRouteName) =>
    Route.Replace("{OrganisationRouteName}", organisationRouteName);
}
