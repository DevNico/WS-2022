using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public record LocaleRecord(int Id, string LanguageCode, string CountryCode, bool IsDefault)
{
  public static LocaleRecord FromEntity(Locale locale)
  {
    return new LocaleRecord(locale.Id, locale.LanguageCode, locale.CountryCode, locale.IsDefault);
  }
}
