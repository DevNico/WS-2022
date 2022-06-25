using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ActiveServiceSearchSpec : Specification<Service>
{
  public ActiveServiceSearchSpec()
  {
    Query.Where(s => s.IsActive);
  }
}
