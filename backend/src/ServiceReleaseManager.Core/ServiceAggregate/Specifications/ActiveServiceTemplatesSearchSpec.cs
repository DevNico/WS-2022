using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ActiveServiceTemplatesSearchSpec : Specification<ServiceTemplate>
{
  public ActiveServiceTemplatesSearchSpec(int organisationId)
  {
    Query
     .Where(s => s.OrganisationId == organisationId)
     .Where(s => s.IsActive);
  }
}
