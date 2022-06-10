namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class ListLocalesByServiceId
{
  public const string Route = "/services/{ServiceId:int}/locales";
  
  public int ServiceId { get; set; }

  public static string BuildRoute(int serviceId)
  {
    return Route.Replace("{ServiceId:int}", serviceId.ToString());
  }
}
