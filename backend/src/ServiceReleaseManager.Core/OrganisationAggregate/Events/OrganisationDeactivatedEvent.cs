using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Events;

public class OrganisationDeactivatedEvent : DomainEventBase
{
  public OrganisationDeactivatedEvent(Organisation organisation)
  {
    Organisation = organisation;
  }

  public Organisation Organisation { get; }
}
