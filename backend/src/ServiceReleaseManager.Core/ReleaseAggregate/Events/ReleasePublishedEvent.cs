using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ReleaseAggregate.Events;

public class ReleasePublishedEvent : DomainEventBase
{
  public ReleasePublishedEvent(Release release, OrganisationUser organisationUser, DateTime at)
  {
    Release = release;
    OrganisationUser = organisationUser;
    At = at;
  }

  public Release Release { get; }
  public OrganisationUser OrganisationUser { get; }
  public DateTime At { get; }
}
