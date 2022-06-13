using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Sepcifications;

public sealed class ActiveServiceTemplatesSearchSpec : Specification<ServiceTemplate>
{
  public ActiveServiceTemplatesSearchSpec()
  {
    Query.Where(s => s.IsActive);
  }
}
