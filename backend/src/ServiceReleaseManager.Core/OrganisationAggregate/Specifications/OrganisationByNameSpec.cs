using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public class OrganisationByNameSpec : Specification<Organisation>, ISingleResultSpecification
{
  public OrganisationByNameSpec(string organisationName)
  {
    Query
      .Where(o => o.IsActive)
      .Where(o => o.Name.Equals(organisationName, StringComparison.OrdinalIgnoreCase))
      .Include(o => o.Roles)
      .Include(o => o.Users);
  }
}
