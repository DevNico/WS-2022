using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationByIdSpec : Specification<Organisation>, ISingleResultSpecification
{
  public OrganisationByIdSpec(int organisationId)
  {
    Query
      .Where(o => o.IsActive)
      .Where(o => o.Id == organisationId);
  }
}
