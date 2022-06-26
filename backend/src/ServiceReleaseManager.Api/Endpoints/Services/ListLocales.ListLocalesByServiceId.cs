using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class ListLocalesByServiceRouteName
{
  public const string Route = "{ServiceRouteName}/locales";

  [FromRoute]
  public string ServiceRouteName { get; set; } = default!;
}
