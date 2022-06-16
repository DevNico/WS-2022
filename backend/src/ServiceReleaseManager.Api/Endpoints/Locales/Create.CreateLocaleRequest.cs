using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.Locales;

public class CreateLocaleRequest
{
  [Required]
  public int ServiceId { get; set; } = default!;

  [Required]
  [StringLength(2)]
  public string? LanguageCode { get; set; } = default!;

  [Required]
  [StringLength(2)]
  public string? CountryCode { get; set; } = default!;

  [DefaultValue(false)]
  public bool? IsDefault { get; set; } = default!;
}
