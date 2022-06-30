using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Events;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class Service : EntityBase, IAggregateRoot
{
  [UsedImplicitly]
  protected Service()
  {
  }

  public Service(
    string name,
    string description,
    ServiceTemplate template,
    Organisation organisation
  )
  {
    Name = name;
    RouteName = Name.Replace(" ", "-").ToLower();
    Description = description;
    ServiceTemplate = template;
    ServiceTemplateId = template.Id;
    Organisation = organisation;
    OrganisationId = organisation.Id;
    IsActive = true;
  }

  [Required]
  [MinLength(5)]
  [MaxLength(50)]
  public string Name { get; set; } = null!;

  public String RouteName { get; } = null!;

  [Required]
  [MaxLength(200)]
  public string Description { get; set; } = null!;

  public ServiceTemplate ServiceTemplate { get; set; } = null!;

  public int ServiceTemplateId { get; set; }

  public List<Locale> Locales { get; set; } = new();

  public List<Release> Releases { get; set; } = new();

  public List<ServiceUser> Users { get; set; } = new();

  public Organisation Organisation { get; set; } = null!;
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
