using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseTriggers;

public class CreateReleaseTriggerRequest
{
  [Required, MaxLength(50)]
  public string Name { get; set; } = default!;

  [Required, MaxLength(50)]
  public string Event { get; set; } = default!;

  [Required, MaxLength(250)]
  public string Url { get; set; } = default!;

  [Required]
  public int ServiceId { get; set; }
}
