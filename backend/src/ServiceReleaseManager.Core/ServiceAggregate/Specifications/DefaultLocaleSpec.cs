using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class DefaultLocaleSpec : Specification<Locale>, ISingleResultSpecification
{
  public DefaultLocaleSpec(int serviceId)
  {
    Query
     .Where(l => l.ServiceId == serviceId)
     .Where(l => l.IsDefault);
  }
}
