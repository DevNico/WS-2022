using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ReleaseAggregate.Specifications;

public sealed class ActiveReleaseByServiceIdSpec : Specification<Release>,
                                                   ISingleResultSpecification<Release>
{
  public ActiveReleaseByServiceIdSpec(int serviceId)
  {
    Query
     .Where(release => release.ServiceId == serviceId)
     .Where(release => release.PublishedAt == null);
  }
}
