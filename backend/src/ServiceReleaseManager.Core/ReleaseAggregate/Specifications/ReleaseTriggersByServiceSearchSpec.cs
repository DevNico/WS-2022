using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ReleaseAggregate.Specifications;

public sealed class ReleaseTriggersByServiceSearchSpec :
  Specification<ReleaseTrigger>
{
  public ReleaseTriggersByServiceSearchSpec(int serviceId)
  {
    Query
     .Include(trigger => trigger.Service);
    Query
     .Where(trigger => trigger.Service.Id == serviceId);
  }
}
