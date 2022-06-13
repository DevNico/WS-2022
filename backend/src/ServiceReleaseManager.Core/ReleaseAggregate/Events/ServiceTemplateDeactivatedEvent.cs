using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ReleaseAggregate.Events;

public class ServiceTemplateDeactivatedEvent : DomainEventBase
{
  public ServiceTemplateDeactivatedEvent(ServiceTemplate serviceTemplate)
  {
    ServiceTemplate = serviceTemplate;
  }
  
  public ServiceTemplate ServiceTemplate { get; }
}
