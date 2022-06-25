namespace ServiceReleaseManager.Api.Endpoints.Releases;

public class GetReleaseByIdRequest
{
  public const string Route = "/releases/{ReleaseId:int}";

  public int ReleaseId { get; set; } = default!;
}
