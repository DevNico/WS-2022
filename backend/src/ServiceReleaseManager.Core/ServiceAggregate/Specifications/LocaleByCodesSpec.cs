using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class LocaleByCodesSpec : Specification<Locale>, ISingleResultSpecification
{
  public LocaleByCodesSpec(string countryCode, string languageCode)
  {
    Query.Where(locale => locale.CountryCode == countryCode && locale.LanguageCode == languageCode);
  }
}
