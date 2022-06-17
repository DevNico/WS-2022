using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Sepcifications;

public sealed class ServiceByIdSpec : Specification<Service>, ISingleResultSpecification
{
  public ServiceByIdSpec(int id)
  {
    Query
      .Where(s => s.IsActive)
      .Where(s => s.Id == id);
  }
}
