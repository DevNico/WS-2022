using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseEndpoints;

public class CreateReleaseRequest
{
  public const string Route = "/releases";

  [Required] public string? Version { get; set; }
  [Required] public int BuildNumber { get; set; }
  [Required] public string? MetaData { get; set; }
}
