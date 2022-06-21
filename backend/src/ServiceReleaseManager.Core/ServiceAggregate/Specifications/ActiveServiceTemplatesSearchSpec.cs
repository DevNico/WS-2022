using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ActiveServiceTemplatesSearchSpec : Specification<ServiceTemplate>
{
  public ActiveServiceTemplatesSearchSpec()
  {
    Query.Where(s => s.IsActive);
  }
}
