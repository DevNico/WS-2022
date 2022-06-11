using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ReleaseAggregate;

public class ReleaseTrigger : EntityBase
{
  public ReleaseTrigger(string name, string @event, string url)
  {
    Name = name;
    Event = @event;
    Url = url;
  }

  [Required] [MaxLength(50)] public string Name { get; set; }

  [Required] [MaxLength(50)] public string Event { get; set; }

  [Required] [MaxLength(250)] public string Url { get; set; }
}
