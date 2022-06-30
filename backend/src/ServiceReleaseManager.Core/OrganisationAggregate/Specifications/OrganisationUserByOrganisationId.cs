using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationUserByOrganisationIdSpec : Specification<OrganisationUser>,
                                                           ISingleResultSpecification
{
  public OrganisationUserByOrganisationIdSpec(int organisationId)
  {
    Query
     .Where(user => user.IsActive)
     .Where(user => user.OrganisationId == organisationId);
  }
}
