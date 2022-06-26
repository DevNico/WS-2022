using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class Locale : EntityBase, IAggregateRoot
{
  public Locale(string tag, bool isDefault, int serviceId)
  {
    Tag = tag;
    IsDefault = isDefault;
    ServiceId = serviceId;
  }

  [Required]
  [DefaultValue(false)]
  public bool IsDefault { get; set; }

  [Required]
  public string Tag { get; set; }

  public int ServiceId { get; set; }
}
