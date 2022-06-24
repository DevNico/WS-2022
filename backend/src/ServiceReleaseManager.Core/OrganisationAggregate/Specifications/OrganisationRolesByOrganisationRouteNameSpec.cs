using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationRolesByOrganisationRouteNameSpec :
  Specification<Organisation, IEnumerable<OrganisationRole>>, ISingleResultSpecification
{
  public OrganisationRolesByOrganisationRouteNameSpec(string organisationRouteName)
  {
    Query
      .Where(organisation => organisation.RouteName == organisationRouteName.ToLower())
      .Include(organisation => organisation.Roles);
    Query.Select(organisation => organisation.Roles);
  }
}
