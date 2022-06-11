using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Events;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class Service : EntityBase, IAggregateRoot
{
  public Service()
  {
    Releases = new List<Release>();
    Locales = new List<Locale>();
    ReleaseTargets = new List<ReleaseTarget>();

    var serviceCreatedEvent = new ServiceCreatedEvent(this);
    RegisterDomainEvent(serviceCreatedEvent);
  }

  [Required] public List<Release> Releases { get; set; }

  [Required] public List<Locale> Locales { get; set; }

  [Required] public List<ReleaseTarget> ReleaseTargets { get; set; }
}
