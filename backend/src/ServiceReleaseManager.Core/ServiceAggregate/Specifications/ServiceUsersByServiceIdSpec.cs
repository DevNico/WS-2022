using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServiceUsersByServiceIdSpec : Specification<ServiceUser>
{
  public ServiceUsersByServiceIdSpec(int serviceId)
  {
    Query.Where(u => u.ServiceId == serviceId);
  }
}
