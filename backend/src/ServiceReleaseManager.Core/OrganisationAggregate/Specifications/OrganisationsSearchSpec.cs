using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationsSearchSpec : Specification<Organisation>
{
  public OrganisationsSearchSpec(bool includeDeactivated = false)
  {
    if (!includeDeactivated)
    {
      Query
        .Where(o => o.IsActive);
    }
  }
}
