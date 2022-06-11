using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Sepcifications;

public sealed class ServiceTemplateByNameSpec : Specification<ServiceTemplate>, ISingleResultSpecification
{
  public ServiceTemplateByNameSpec(string name)
  {
    Query
      .Where(s => s.IsActive)
      .Where(s => s.Name == name);
  }
}
