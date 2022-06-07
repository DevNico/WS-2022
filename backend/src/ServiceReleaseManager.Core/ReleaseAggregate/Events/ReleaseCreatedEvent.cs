using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ReleaseAggregate.Events;

public class ReleaseCreatedEvent : DomainEventBase
{
  public ReleaseCreatedEvent(Release release)
  {
    Release = release;
  } 
  
  public Release Release { get; }
}
