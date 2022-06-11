using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Events;

public class OrganisationUserCreatedEvent : DomainEventBase
{
  public OrganisationUserCreatedEvent(OrganisationUser organisationUser)
  {
    OrganisationUser = organisationUser;
  }

  public OrganisationUser OrganisationUser { get; }
}
