using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class ListReleaseTriggersRequest
{
  public const string Route = "{ServiceId}/release-triggers";

  [Required]
  [FromRoute]
  public int ServiceId { get; set; } = default!;
}

