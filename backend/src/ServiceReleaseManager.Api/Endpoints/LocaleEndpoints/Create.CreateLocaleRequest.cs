using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class CreateLocaleRequest
{
  public const string Route = "/services/{ServiceId:int}/locales";

  public int ServiceId { get; set; }
  [DefaultValue(false)] public bool? IsDefault { get; set; }
  [Required] [MaxLength(3)] public string? LanguageCode { get; set; }
  [Required] [MaxLength(3)] public string? CountryCode { get; set; }

  public static string BuildRoute(int serviceId)
  {
    return Route.Replace("{ServiceId:int}", serviceId.ToString());
  }
}
