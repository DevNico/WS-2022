using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Events;

public class OrganisationUserDeactivatedEvent : DomainEventBase
{
  public OrganisationUserDeactivatedEvent(OrganisationUser organisationUser)
  {
    OrganisationUser = organisationUser;
  }

  public OrganisationUser OrganisationUser { get; }
}
