using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class CreateLocaleRequest
{
  public const string Route = "/locales";
  
  [DefaultValue(false)] public bool? IsDefault { get; set; }
  [Required] [MaxLength(3)] public string? LanguageCode { get; set; }
  [Required] [MaxLength(3)] public string? CountryCode { get; set; }
}
