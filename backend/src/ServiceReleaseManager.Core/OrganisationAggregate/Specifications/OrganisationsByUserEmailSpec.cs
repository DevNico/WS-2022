using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationsByUserEmailSpec : Specification<OrganisationUser, List<Organisation>>
{
  public OrganisationsByUserEmailSpec(string email)
  {
    Query.Include(user => user.Organisation);
    Query.Where(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    Query.Select(user => user.);
  }
}
