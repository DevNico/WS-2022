using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServiceByOrganisationSearchSpec :
  Specification<Service>
{
  public ServiceByOrganisationSearchSpec(string organisationRouteName)
  {
    Query
      .Include(service => service.Organisation);
    Query
      .Where(service => service.IsActive)
      .Where(service => service.Organisation.RouteName == organisationRouteName.ToLower());
  }
}
