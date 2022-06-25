using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ServiceAggregate.Events;

public class ServiceRoleDeactivatedEvent : DomainEventBase
{
  public ServiceRoleDeactivatedEvent(ServiceRole serviceRole)
  {
    ServiceRole = serviceRole;
  }

  public ServiceRole ServiceRole { get; }
}
