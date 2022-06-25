using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServiceRolesByOrganisationIdSpec : Specification<ServiceRole>
{
  public ServiceRolesByOrganisationIdSpec(int organisationId)
  {
    Query
      .Where(r => r.IsActive)
      .Where(r => r.OrganisationId == organisationId);
  }
}
