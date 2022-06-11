using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationUserByUserIdSpec : Specification<OrganisationUser>,
  ISingleResultSpecification
{
  public OrganisationUserByUserIdSpec(string userId)
  {
    Query
      .Where(u => u.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase));
  }
}
