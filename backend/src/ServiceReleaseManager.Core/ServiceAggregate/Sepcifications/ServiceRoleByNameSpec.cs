using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Sepcifications;

public sealed class ServiceRoleByNameSpec : Specification<ServiceRole>, ISingleResultSpecification
{
  public ServiceRoleByNameSpec(string name)
  {
    Query
      .Where(s => s.Name == name);
  }
}
