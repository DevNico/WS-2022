using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class ListLocalesByServiceId
{
  public const string Route = "/services/{ServiceId:int}/locales";

  [FromRoute]
  public int ServiceId { get; set; } = default!;

  public static string BuildRoute(int serviceId)
  {
    return Route.Replace("{ServiceId:int}", serviceId.ToString());
  }
}
