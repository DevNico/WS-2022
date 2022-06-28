using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ReleaseAggregate;

public class ReleaseTrigger : EntityBase, IAggregateRoot
{
  protected ReleaseTrigger()
  {
    
  }
  
  public ReleaseTrigger(string name, string @event, string url, Service service)
  {
    Name = name;
    Event = @event;
    Url = url;
    Service = service;
  }

  [Required]
  [MaxLength(50)]
  public string Name { get; set; }

  [Required]
  [MaxLength(50)]
  public string Event { get; set; }

  [Required]
  [MaxLength(250)]
  public string Url { get; set; }
  
  [Required]
  public Service Service { get; set; }
}
