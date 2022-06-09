using ServiceReleaseManager.Core.ReleaseAggregate;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public record LocaleRecord(int Id, string languageCode, string countryCode, bool isDefault)
{
  public static LocaleRecord FromEntity(Locale locale)
  {
    return new LocaleRecord(locale.Id, locale.LanguageCode, locale.CountryCode, locale.IsDefault);
  }
}
