using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Releases;

public class GetReleaseByIdRequest
{
  public const string Route = "{ReleaseId:int}";

  [FromRoute]
  [Required]
  public int ReleaseId { get; set; } = default!;
}
