using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServiceTemplateByNameSpec : Specification<ServiceTemplate>,
                                                ISingleResultSpecification
{
  public ServiceTemplateByNameSpec(string name, bool active = true)
  {
    Query
     .Where(s => s.IsActive == active)
     .Where(s => s.Name == name.ToLower().Trim());
  }
}
