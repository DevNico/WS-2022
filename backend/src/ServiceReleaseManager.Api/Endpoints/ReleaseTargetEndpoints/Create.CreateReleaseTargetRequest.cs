using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseTargetEndpoints;

public class CreateReleaseTargetRequest
{
  public const string Route = "/release-targets";

  [Required] public string? Name { get; set; }
  [Required] public bool RequiresApproval { get; set; }
}
