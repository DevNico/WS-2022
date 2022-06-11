using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public class OrganisationUserByIdSpec : Specification<OrganisationUser>, ISingleResultSpecification
{
  public OrganisationUserByIdSpec(int id)
  {
    Query
      .Where(r => r.Id == id);
  }
}
