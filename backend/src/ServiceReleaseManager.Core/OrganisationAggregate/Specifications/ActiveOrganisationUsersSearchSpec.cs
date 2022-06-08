using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class ActiveOrganisationUsersSearchSpec : Specification<OrganisationUser>
{
  public ActiveOrganisationUsersSearchSpec()
  {
    Query
      .Where(o => o.IsActive);
  }
}
