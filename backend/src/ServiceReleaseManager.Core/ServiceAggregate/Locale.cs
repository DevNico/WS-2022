using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class Locale : EntityBase, IAggregateRoot
{
  public Locale(string countryCode, string languageCode, bool isDefault, int serviceId)
  {
    CountryCode = countryCode;
    LanguageCode = languageCode;
    IsDefault = isDefault;
    ServiceId = serviceId;
  }

  [Required]
  [DefaultValue(false)]
  public bool IsDefault { get; set; }

  [Required]
  [StringLength(2)]
  public string CountryCode { get; set; }

  [Required]
  [StringLength(2)]
  public string LanguageCode { get; set; }

  public int ServiceId { get; set; }
}
