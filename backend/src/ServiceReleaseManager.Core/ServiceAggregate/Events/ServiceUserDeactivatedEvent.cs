using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ServiceAggregate.Events;

public class ServiceUserDeactivatedEvent : DomainEventBase
{
  public ServiceUserDeactivatedEvent(ServiceUser serviceUser)
  {
    ServiceUser = serviceUser;
  }

  public ServiceUser ServiceUser { get; }
}
