using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class ListUsersByServiceId
{
  public const string Route = $"{{ServiceId:int}}/users";

  [FromRoute]
  public int ServiceId { get; set; } = default!;
}
