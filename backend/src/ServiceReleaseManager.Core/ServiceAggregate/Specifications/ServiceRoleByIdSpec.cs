using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServiceRoleByIdSpec : Specification<ServiceRole>, ISingleResultSpecification
{
  public ServiceRoleByIdSpec(int id)
  {
    Query
      .Where(r => r.IsActive)
      .Where(r => r.Id == id);
  }
}
