namespace ServiceReleaseManager.Api.Endpoints.ReleaseEndpoints;

public class GetReleaseByIdRequest
{
  public const string Route = "/releases/{ReleaseId:int}";

  public int ReleaseId { get; set; }

  public static string BuildRoute(int releaseId)
  {
    return Route.Replace("{ReleaseId:int}", releaseId.ToString());
  }
}
