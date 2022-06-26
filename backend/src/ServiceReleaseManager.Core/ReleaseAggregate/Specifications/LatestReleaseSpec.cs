using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ReleaseAggregate.Specifications;

public sealed class LatestReleaseSpec : Specification<Release>, ISingleResultSpecification
{
  public LatestReleaseSpec()
  {
    Query
      .OrderByDescending(release => release.PublishedAt)
      .Take(1);
  }
}
