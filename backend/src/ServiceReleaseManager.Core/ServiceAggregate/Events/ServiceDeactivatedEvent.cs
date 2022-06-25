using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ServiceAggregate.Events;

public class ServiceDeactivatedEvent : DomainEventBase
{
  public ServiceDeactivatedEvent(Service service)
  {
    Service = service;
  }

  public Service Service { get; }
}
