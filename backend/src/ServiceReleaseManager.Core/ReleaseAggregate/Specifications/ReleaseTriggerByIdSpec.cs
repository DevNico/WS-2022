using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ReleaseAggregate.Specifications;

public sealed class ReleaseTriggerByIdSpec : Specification<ReleaseTrigger>,
                                             ISingleResultSpecification
{
  public ReleaseTriggerByIdSpec(int id)
  {
    Query
     .Where(trigger => trigger.Id == id)
     .Include(trigger => trigger.Service);
  }
}
