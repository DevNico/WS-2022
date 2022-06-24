namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListOrganisationRolesRequest
{
  public const string Route = "{OrganisationRouteName}/roles";

  public string OrganisationRouteName { get; set; } = default!;
}
