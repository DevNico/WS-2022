using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServicesByOrganisationUserIdsSpec : Specification<Service>
{
  public ServicesByOrganisationUserIdsSpec(ICollection<int> organisationUserIds)
  {
    Query
      .Where(s => s.IsActive)
      .Include(s => s.Users)
      .Where(s => s.Users.Exists(u => organisationUserIds.Contains(u.OrganisationUserId)));
  }
}
