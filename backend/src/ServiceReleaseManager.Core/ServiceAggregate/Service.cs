using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Events;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class Service : EntityBase, IAggregateRoot
{
  // Used by EF
  protected Service()
  {
  }

  public Service(
    string name,
    string description,
    int serviceTemplateId,
    int organisationId
  )
  {
    Name = name;
    RouteName = Name.Replace(" ", "-").ToLower();
    Description = description;
    ServiceTemplateId = serviceTemplateId;
    OrganisationId = organisationId;
    IsActive = true;
  }

  [Required]
  [MinLength(5)]
  [MaxLength(50)]
  public string Name { get; set; }

  public String RouteName { get; }

  [Required]
  [MaxLength(200)]
  public string Description { get; set; }

  public ServiceTemplate ServiceTemplate { get; set; }

  public int ServiceTemplateId { get; set; }

  public List<Locale> Locales { get; set; } = new();

  public List<Release> Releases { get; set; } = new();

  public List<ServiceUser> Users { get; set; } = new();

  public Organisation Organisation { get; set; }
  public int OrganisationId { get; set; }

  [DefaultValue(true)]
  public bool IsActive { get; set; }

  public void Deactivate()
  {
    IsActive = false;
    var serviceTemplateDeactivatedEvent = new ServiceDeactivatedEvent(this);
    RegisterDomainEvent(serviceTemplateDeactivatedEvent);
  }
}
