namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class GetOrganisationRoleByNameRequest
{
  public const string Route = "/organisations/{Name:string}";

  public string? Name { get; set; }

  public static string BuildRoute(string name)
  {
    return Route.Replace("{Name:string}", name );
  }
}
