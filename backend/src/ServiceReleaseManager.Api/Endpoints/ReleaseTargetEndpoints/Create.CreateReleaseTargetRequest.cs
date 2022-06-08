using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.RelaseTargetEndpoints;

public class CreateReleaseTargetRequest
{
  public const string Route = "/release-targets";

  [Required] public string? Name { get; set; }
  [Required] public bool RequiresApproval { get; set; }
}
