namespace ServiceReleaseManager.Api.Endpoints.ServiceRoles;

public class DeleteServiceRole
{
  public const string Route = "{ServiceRoleId:int}";

  public int ServiceRoleId { get; set; }
}
