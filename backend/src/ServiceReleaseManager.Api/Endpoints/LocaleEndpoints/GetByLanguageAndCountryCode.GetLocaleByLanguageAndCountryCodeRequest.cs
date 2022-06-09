namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class GetLocaleByLanguageAndCountryCodeRequest
{
  public const string Route = "/locales/{LocaleLanguage}-{LocaleCountry}";

  public string LocaleLanguage { get; set; }
  public string LocaleCountry { get; set; }

  public static string BuildRoute(string localeLanguage, string localeCountry)
  {
    return Route.Replace("{LocaleLanguage}", localeLanguage)
      .Replace("{LocaleCountry}", localeCountry);
  }
}
