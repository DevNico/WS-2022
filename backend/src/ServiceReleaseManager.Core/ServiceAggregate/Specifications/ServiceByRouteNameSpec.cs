using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServiceByRouteNameSpec : Specification<Service>, ISingleResultSpecification
{
  public ServiceByRouteNameSpec(string routeName)
  {
    Query
      .Where(s => s.IsActive)
      .Where(s => s.RouteName == routeName.ToLower())
      .Include(s => s.Releases)
      .Include(s => s.Locales);
  }
}
