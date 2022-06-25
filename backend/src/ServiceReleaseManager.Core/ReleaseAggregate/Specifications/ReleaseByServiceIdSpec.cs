using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ReleaseAggregate.Specifications;

public sealed class ReleaseByServiceIdSpec : Specification<Release>, ISingleResultSpecification
{
  public ReleaseByServiceIdSpec(int serviceId)
  {
    Query
      .Where(o => o.ServiceId == serviceId);
  }
}
