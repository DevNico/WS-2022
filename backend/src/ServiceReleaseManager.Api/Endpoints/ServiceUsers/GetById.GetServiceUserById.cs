namespace ServiceReleaseManager.Api.Endpoints.ServiceUsers;

public class GetServiceUserById
{
  public const string Route = "{ServiceUserId:int}";

  public int ServiceUserId { get; set; }
}
