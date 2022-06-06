using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ReleaseAggregate;

public class Locale : EntityBase
{
  public Locale(string languageCode, string countryCode, bool @default = false)
  {
    LanguageCode = languageCode;
    CountryCode = countryCode;
    Default = @default;
  }
  
  [Required]
  [DefaultValue(false)]
  public bool Default { get; set; }
  
  [Required]
  [MaxLength(2)]
  public string LanguageCode { get; set; }
  
  [Required]
  [MaxLength(2)]
  public string CountryCode { get; set; }
}
