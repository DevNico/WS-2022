using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class GetServiceByRouteNameRequest
{
  public const string Route = "{ServiceRouteName}";

  [Required]
  public string ServiceRouteName { get; set; } = default!;
}
