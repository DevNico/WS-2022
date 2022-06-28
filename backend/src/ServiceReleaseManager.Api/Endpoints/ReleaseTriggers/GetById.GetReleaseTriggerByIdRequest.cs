using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseTriggers;

public class GetReleaseTriggerByIdRequest
{
  public const string Route = "{ReleaseTriggerId:int}";

  [Required]
  public int ReleaseTriggerId { get; set; }
}
