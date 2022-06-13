using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ReleaseAggregate.Events;

public class ReleasePublishedEvent : DomainEventBase
{
  public ReleasePublishedEvent(Release release, OrganisationUser organisationUser,
    DateTime publishedAt)
  {
    Release = release;
    OrganisationUser = organisationUser;
    PublishedAt = publishedAt;
  }

  public Release Release { get; }
  public OrganisationUser OrganisationUser { get; }
  public DateTime PublishedAt { get; }
}
