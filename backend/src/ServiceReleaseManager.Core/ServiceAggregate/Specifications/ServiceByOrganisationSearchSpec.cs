using Ardalis.Specification;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServiceByOrganisationSearchSpec : 
  Specification<Organisation, IEnumerable<Service>>, ISingleResultSpecification
{
  public ServiceByOrganisationSearchSpec(string name)
  {
    Query
      .Where(organisation => organisation.RouteName == name)
      .Include(organisation => organisation.Services);
    Query
      .Select(organisation => organisation.Services);
  }
}
