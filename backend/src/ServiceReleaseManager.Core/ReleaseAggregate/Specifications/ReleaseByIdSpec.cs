using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ReleaseAggregate.Specifications;

public sealed class ReleaseByIdSpec : Specification<Release>, ISingleResultSpecification
{
  public ReleaseByIdSpec(int releaseId)
  {
    Query
      .Where(o => o.Id == releaseId);
  }
}
