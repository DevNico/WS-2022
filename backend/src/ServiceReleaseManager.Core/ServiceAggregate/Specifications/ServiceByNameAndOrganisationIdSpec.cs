using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServiceByNameAndOrganisationIdSpec : Specification<Service>,
  ISingleResultSpecification
{
  public ServiceByNameAndOrganisationIdSpec(string name, int organisationId)
  {
    Query
      .Where(s => s.IsActive)
      .Where(s => s.Name == name)
      .Where(s => s.OrganisationId == organisationId);
  }
}
