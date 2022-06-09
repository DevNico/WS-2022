namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class DeleteLocaleRequest
{
  public const string Route = "/locales/{LocaleId:int}";
  
  public int LocaleId { get; set; }
  
  public static string BuildRoute(int localeId)
  {
    return Route.Replace("{LocaleId:int}", localeId.ToString());
  }
}
