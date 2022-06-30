using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationUserByIdSpec : Specification<OrganisationUser>,
                                               ISingleResultSpecification
{
  public OrganisationUserByIdSpec(int id)
  {
    Query
     .Where(user => user.IsActive)
     .Where(organisationUser => organisationUser.Id == id);
  }
}
