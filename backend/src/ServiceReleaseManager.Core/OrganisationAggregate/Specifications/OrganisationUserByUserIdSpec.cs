using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationUserByUserIdSpec : Specification<OrganisationUser>, ISingleResultSpecification
{

  public OrganisationUserByUserIdSpec(string userId)
  {
    Query
      .Where(r => r.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase));
  }
}
