using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class LocaleByTagSpec : Specification<Locale>, ISingleResultSpecification
{
  public LocaleByTagSpec(int serviceId, string tag)
  {
    Query
     .Where(locale => locale.ServiceId == serviceId)
     .Where(locale => locale.Tag == tag);
  }
}
