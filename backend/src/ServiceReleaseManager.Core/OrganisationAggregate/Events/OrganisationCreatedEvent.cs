using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Events;

public class OrganisationCreatedEvent : DomainEventBase
{
  public OrganisationCreatedEvent(Organisation organisation)
  {
    Organisation = organisation;
  }

  public Organisation Organisation { get; }
}
