using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationsByUserEmailSpec : Specification<Organisation>
{
  public OrganisationsByUserEmailSpec(string email)
  {
    Query.Include(org => org.Users);
    Query.Where(org => org.Users.Any(user => user.Email == email));
  }
}
