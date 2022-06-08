namespace ServiceReleaseManager.Api.Endpoints.ReleaseTargetEndpoints;

public class GetReleaseTargetByIdRequest
{
  public const string Route = "/release-targets/{ReleaseTargetId:int}";

  public int ReleaseTargetId { get; set; }

  public static string BuildRoute(int releaseTargetId)
  {
    return Route.Replace("{ReleaseTargetId:int}", releaseTargetId.ToString());
  }
}
