using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class LocalesByServiceIdSpec : Specification<Locale>
{
  public LocalesByServiceIdSpec(int serviceId)
  {
    Query.Where(locale => locale.ServiceId == serviceId);
  }
}
