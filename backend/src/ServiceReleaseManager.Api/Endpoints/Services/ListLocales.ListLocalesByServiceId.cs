using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class ListLocalesByServiceId
{
  public const string Route = "{ServiceId:int}/locales";

  [FromRoute]
  public int ServiceId { get; set; } = default!;
}
