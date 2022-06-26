using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServiceByIdSpec : Specification<Service>, ISingleResultSpecification
{
  public ServiceByIdSpec(int id)
  {
    Query
      .Where(s => s.IsActive)
      .Where(s => s.Id == id)
      .Include(s => s.Releases)
      .Include(s => s.Locales);
  }
}
