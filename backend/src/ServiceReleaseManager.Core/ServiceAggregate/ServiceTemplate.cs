using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Events;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class ServiceTemplate : EntityBase, IAggregateRoot
{
  [UsedImplicitly]
  protected ServiceTemplate()
  {
  }

  public ServiceTemplate(
    string name,
    string staticMetadata,
    string localizedMetadata,
    int organisationId
  )
  {
    Name = name.Trim().ToLower();
    StaticMetadata = staticMetadata;
    LocalizedMetadata = localizedMetadata;
    OrganisationId = organisationId;
    IsActive = true;
  }

  [Required]
  [MaxLength(50)]
  public string Name { get; set; } = null!;


  [Required]
  [Column(TypeName = "json")]
  public string StaticMetadata { get; set; } = null!;

  [Required]
  [Column(TypeName = "json")]
  public string LocalizedMetadata { get; set; } = null!;

  [Required]
  [DefaultValue(true)]
  public bool IsActive { get; set; }

  public Organisation Organisation { get; set; } = null!;

  public int OrganisationId { get; set; }

  public void Deactivate()
  {
    IsActive = false;
    var serviceTemplateDeactivatedEvent = new ServiceTemplateDeactivatedEvent(this);
    RegisterDomainEvent(serviceTemplateDeactivatedEvent);
  }
}
