namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class GetLocaleByIdRequest
{
  public const string Route = "/locales/{LocaleId}";
  
  public int LocaleId { get; set; }
  
  public static string BuildRoute(int localeId)
  {
    return Route.Replace("{LocaleId}", localeId.ToString());
  }
}
