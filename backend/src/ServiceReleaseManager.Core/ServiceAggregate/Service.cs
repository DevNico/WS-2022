using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Events;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class Service : EntityBase, IAggregateRoot
{
  public Service(string name, string description, int organisationId)
  {
    Name = name;
    Description = description;
    OrganisationId = organisationId;
    IsActive = true;
  }

  [Required]
  [MinLength(5)]
  [MaxLength(50)]
  public string Name { get; set; }

  [Required]
  [MaxLength(200)]
  public string Description { get; set; }

  public List<Locale> Locales { get; set; } = new();

  public List<Release> Releases { get; set; } = new();

  public int OrganisationId { get; set; }
  
  [DefaultValue(true)] public bool IsActive { get; set; }
  
  public void Deactivate()
  {
    IsActive = false;
    var serviceTemplateDeactivatedEvent = new ServiceDeactivatedEvent(this);
    RegisterDomainEvent(serviceTemplateDeactivatedEvent);
  }
}
