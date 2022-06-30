using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationByRouteNameSpec : Specification<Organisation>,
                                                  ISingleResultSpecification
{
  public OrganisationByRouteNameSpec(string routeName)
  {
    Query
     .Where(o => o.IsActive)
     .Where(o => o.RouteName == routeName.ToLower())
     .Include(o => o.Roles)
     .Include(o => o.Users);
  }
}
