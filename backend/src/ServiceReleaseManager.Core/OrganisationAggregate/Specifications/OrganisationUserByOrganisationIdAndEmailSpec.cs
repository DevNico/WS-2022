using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationUserByOrganisationIdAndEmailSpec : Specification<OrganisationUser>,
                                                                   ISingleResultSpecification
{
  public OrganisationUserByOrganisationIdAndEmailSpec(int organisationId, string email)
  {
    Query
     .Where(user => user.IsActive)
     .Where(user => user.OrganisationId == organisationId)
     .Where(user => user.Email == email);
  }
}
