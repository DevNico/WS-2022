using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServiceUserByIdSpec : Specification<ServiceUser>, ISingleResultSpecification
{
  public ServiceUserByIdSpec(int id)
  {
    Query
      .Where(u => u.IsActive)
      .Where(u => u.Id == id);
  }
}
