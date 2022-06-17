using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Events;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class Service : EntityBase, IAggregateRoot
{
  public Service(string name)
  {
    Name = name;
    Releases = new List<Release>();
    Locales = new List<Locale>();
    ReleaseTargets = new List<ReleaseTarget>();
    IsActive = true;

    var serviceCreatedEvent = new ServiceCreatedEvent(this);
    RegisterDomainEvent(serviceCreatedEvent);
  }

  [Required] [MaxLength(50)] public string Name { get; set; }

  [Required] public List<Release> Releases { get; set; }

  [Required] public List<Locale> Locales { get; set; }

  [Required] public List<ReleaseTarget> ReleaseTargets { get; set; }
  
  [Required] [DefaultValue(true)] public bool IsActive { get; set; }
  
  public void Deactivate()
  {
    IsActive = false;
    var serviceTemplateDeactivatedEvent = new ServiceDeactivatedEvent(this);
    RegisterDomainEvent(serviceTemplateDeactivatedEvent);
  }
}
