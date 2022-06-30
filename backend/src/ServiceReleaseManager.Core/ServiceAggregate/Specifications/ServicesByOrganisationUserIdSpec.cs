using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServicesByOrganisationUserIdSpec : Specification<Service>
{
  public ServicesByOrganisationUserIdSpec(int organisationUserId)
  {
    Query
     .Where(s => s.IsActive)
     .Include(s => s.Users)
     .Where(s => s.Users.Exists(u => u.OrganisationUserId == organisationUserId));
  }
}
