using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationUserByUserIdSpec : Specification<OrganisationUser>,
  ISingleResultSpecification
{
  public OrganisationUserByUserIdSpec(string userId)
  {
    Query
      .Where(user => user.IsActive)
      .Where(user => user.UserId.Equals(userId))
      .Include(user => user.Role);
  }
}
