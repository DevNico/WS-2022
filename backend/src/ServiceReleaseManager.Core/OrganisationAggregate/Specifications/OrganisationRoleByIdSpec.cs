using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationRoleByIdSpec : Specification<OrganisationRole>,
                                               ISingleResultSpecification
{
  public OrganisationRoleByIdSpec(int id)
  {
    Query
     .Where(r => r.Id == id);
  }
}
