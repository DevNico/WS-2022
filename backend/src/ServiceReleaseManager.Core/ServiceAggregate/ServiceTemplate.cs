using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceReleaseManager.Core.ReleaseAggregate.Events;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class ServiceTemplate : EntityBase, IAggregateRoot
{
  public ServiceTemplate(string name, string staticMetadata, string localizedMetadata)
  {
    Name = name;
    StaticMetadata = staticMetadata;
    LocalizedMetadata = localizedMetadata;
    IsActive = true;
  }

  [Required]
  [MaxLength(50)]
  public string Name { get; set; }

  [Required]
  [Column(TypeName = "json")]
  public string StaticMetadata { get; set; }

  [Required]
  [Column(TypeName = "json")]
  public string LocalizedMetadata { get; set; }

  [Required]
  [DefaultValue(true)]
  public bool IsActive { get; set; }

  public void Deactivate()
  {
    IsActive = false;
    var serviceTemplateDeactivatedEvent = new ServiceTemplateDeactivatedEvent(this);
    RegisterDomainEvent(serviceTemplateDeactivatedEvent);
  }
}
