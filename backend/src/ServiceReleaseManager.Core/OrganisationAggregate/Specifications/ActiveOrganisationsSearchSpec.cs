using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class ActiveOrganisationsSearchSpec : Specification<Organisation>
{
  public ActiveOrganisationsSearchSpec()
  {
    Query
      .Where(o => o.IsActive);
  }
}
