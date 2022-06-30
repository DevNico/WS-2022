using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServiceRoleByNameSpec : Specification<ServiceRole>
{
  public ServiceRoleByNameSpec(string name)
  {
    Query
     .Where(s => s.IsActive)
     .Where(s => s.Name == name);
  }
}
