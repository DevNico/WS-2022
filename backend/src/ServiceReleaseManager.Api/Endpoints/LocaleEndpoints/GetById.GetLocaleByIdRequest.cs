using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class GetLocaleByIdRequest
{
  public const string Route = "/locales/{LocaleId:int}";

  [FromRoute]
  [Required]
  public int LocaleId { get; set; } = default!;

  public static string BuildRoute(int localeId)
  {
    return Route.Replace("{LocaleId:int}", localeId.ToString());
  }
}
