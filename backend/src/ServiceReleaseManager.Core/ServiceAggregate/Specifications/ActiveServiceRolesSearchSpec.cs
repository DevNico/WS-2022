using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ActiveServiceRolesSearchSpec : Specification<ServiceRole>
{
  public ActiveServiceRolesSearchSpec()
  {
    Query.Where(r => r.IsActive);
  }
}
