using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ServiceAggregate.Events;

public class ServiceCreatedEvent : DomainEventBase
{
  public ServiceCreatedEvent(Service service)
  {
    Service = service;
  }

  public Service Service { get; }
}
