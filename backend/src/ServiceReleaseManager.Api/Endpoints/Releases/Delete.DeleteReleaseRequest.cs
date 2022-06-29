namespace ServiceReleaseManager.Api.Endpoints.ReleaseEndpoints;

public class DeleteReleaseReqest
{
  public const string Route = "{ReleaseId:int}";

  public int ReleaseId { get; set; }

  public static string BuildRoute(int releaseId)
  {
    return Route.Replace("{ReleaseId:int}", releaseId.ToString());
  }
}
