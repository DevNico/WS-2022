using ServiceReleaseManager.Core.ReleaseAggregate;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseTriggers;

public record ReleaseTriggerRecord(
  int Id,
  string Name,
  string Event,
  string Url,
  int ServiceId
)
{
  public static ReleaseTriggerRecord FromEntity(ReleaseTrigger trigger)
  {
    return new ReleaseTriggerRecord(trigger.Id, trigger.Name, trigger.Event, trigger.Url,
      trigger.Service.Id);
  }
}
