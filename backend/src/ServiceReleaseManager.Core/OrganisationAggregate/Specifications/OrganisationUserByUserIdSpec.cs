using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationUserByUserIdSpec : Specification<OrganisationUser>
{
  public OrganisationUserByUserIdSpec(string userId)
  {
    Query
      .Where(user => user.IsActive)
      .Where(user => user.UserId == userId);
  }
}
