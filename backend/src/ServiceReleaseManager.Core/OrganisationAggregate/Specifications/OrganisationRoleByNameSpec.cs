using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
public sealed class OrganisationRoleByNameSpec : Specification<OrganisationRole>, ISingleResultSpecification
{

  public OrganisationRoleByNameSpec(string name)
  {
    Query
      .Where(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
  }
}
