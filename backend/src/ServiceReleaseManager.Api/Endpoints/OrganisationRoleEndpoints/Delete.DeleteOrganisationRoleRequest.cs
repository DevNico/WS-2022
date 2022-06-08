namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class DeleteOrganisationRoleRequest
{
  public const string Route = "/organisationroles/{Name:string}";

  public string? Name { get; set; }

  public static string BuildRoute(string name)
  {
    return Route.Replace("{Name:string}", name);
  }

  
}
