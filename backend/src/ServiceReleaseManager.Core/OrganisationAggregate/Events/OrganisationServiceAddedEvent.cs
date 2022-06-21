using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Events;

public class OrganisationServiceAddedEvent : DomainEventBase
{
  public OrganisationServiceAddedEvent(Organisation organisation, Service service)
  {
    Organisation = organisation;
    Service = service;
  }

  public Organisation Organisation { get; }
  public Service Service { get; }
}
