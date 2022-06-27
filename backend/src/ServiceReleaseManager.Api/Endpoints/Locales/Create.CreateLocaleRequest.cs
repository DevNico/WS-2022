using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.Locales;

public class CreateLocaleRequest
{
  [Required]
  public int ServiceId { get; set; } = default!;

  [Required]
  public string Tag { get; set; } = default!;

  [DefaultValue(false)]
  public bool? IsDefault { get; set; } = default!;
}
