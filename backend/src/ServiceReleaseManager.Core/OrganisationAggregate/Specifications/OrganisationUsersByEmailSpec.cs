using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationUsersByEmailSpec : Specification<OrganisationUser>
{
  public OrganisationUsersByEmailSpec(string email)
  {
    Query
      .Where(u => u.IsActive)
      .Where(u => u.Email == email);
  }
}
