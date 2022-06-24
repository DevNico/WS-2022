using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Locales;

public class DeleteLocaleRequest
{
  public const string Route = "{LocaleId:int}";

  [FromRoute]
  [Required]
  public int LocaleId { get; set; } = default!;
}
