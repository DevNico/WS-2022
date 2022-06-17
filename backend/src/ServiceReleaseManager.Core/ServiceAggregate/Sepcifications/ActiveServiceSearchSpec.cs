using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Sepcifications;

public sealed class ActiveServiceSearchSpec : Specification<Service>
{
  public ActiveServiceSearchSpec()
  {
    Query.Where(s => s.IsActive);
  }
}
