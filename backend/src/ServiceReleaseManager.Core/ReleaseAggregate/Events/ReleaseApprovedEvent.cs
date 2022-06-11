using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ReleaseAggregate.Events;

public class ReleaseApprovedEvent : DomainEventBase
{
  public ReleaseApprovedEvent(Release release, OrganisationUser organisationUser, DateTime at)
  {
    Release = release;
    OrganisationUser = organisationUser;
    At = at;
  }

  public Release Release { get; }
  public OrganisationUser OrganisationUser { get; }
  public DateTime At { get; }
}
