namespace ServiceReleaseManager.Api.Endpoints.ServiceRoles;

public class GetServiceRoleById
{
  public const string Route = "{ServiceRoleId:int}";
  
  public int ServiceRoleId { get; set; }
}
