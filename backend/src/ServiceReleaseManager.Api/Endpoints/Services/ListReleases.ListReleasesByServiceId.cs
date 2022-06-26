using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class ListReleasesByServiceId
{
  public const string Route = "{ServiceId:int}/releases";

  [FromRoute]
  public int ServiceId { get; set; } = default!;
}
