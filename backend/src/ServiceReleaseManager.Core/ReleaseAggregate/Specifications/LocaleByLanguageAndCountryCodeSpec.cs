using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ReleaseAggregate.Specifications;

public sealed class LocaleByLanguageAndCountryCodeSpec : Specification<Locale>, ISingleResultSpecification
{
  public LocaleByLanguageAndCountryCodeSpec(string languageCode, string countryCode)
  {
    Query
      .Where(l => l.CountryCode == countryCode)
      .Where(l => l.LanguageCode == languageCode);
  }
}
