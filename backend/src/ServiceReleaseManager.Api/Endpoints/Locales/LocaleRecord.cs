using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Api.Endpoints.Locales;

public record LocaleRecord(int Id, string Tag, bool IsDefault)
{
  public static LocaleRecord FromEntity(Locale locale)
  {
    return new LocaleRecord(locale.Id, locale.Tag, locale.IsDefault);
  }
}
