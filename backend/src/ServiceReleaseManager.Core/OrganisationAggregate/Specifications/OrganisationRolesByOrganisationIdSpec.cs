using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationRolesByOrganisationIdSpec :
  Specification<Organisation, IEnumerable<OrganisationRole>>, ISingleResultSpecification
{
  public OrganisationRolesByOrganisationIdSpec(int organisationId)
  {
    Query
      .Where(organisation => organisation.Id == organisationId)
      .Include(organisation => organisation.Roles);
    Query.Select(organisation => organisation.Roles);
  }
}
