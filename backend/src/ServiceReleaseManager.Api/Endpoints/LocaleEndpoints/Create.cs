using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class Create : EndpointBaseAsync.WithRequest<CreateLocaleRequest>.WithActionResult<
  LocaleRecord>
{
  private readonly ILocaleService _localeService;

  public Create(ILocaleService localeService)
  {
    _localeService = localeService;
  }

  [Authorize]
  [HttpPost(CreateLocaleRequest.Route)]
  [SwaggerOperation(
    Summary = "Create a new locale",
    Description = "Create a new locale",
    OperationId = "Locale.Create",
    Tags = new[] { "Locale" }
  )]
  public override async Task<ActionResult<LocaleRecord>> HandleAsync(
    [FromBody] CreateLocaleRequest request,
    CancellationToken cancellationToken = new())
  {
    if (request.LanguageCode == null || request.CountryCode == null)
    {
      return BadRequest();
    }

    var locale = await _localeService.Create(
      new Locale(request.CountryCode, request.LanguageCode,
        request.IsDefault.GetValueOrDefault(false), request.ServiceId), cancellationToken);

    return this.ToActionResult(locale.MapValue(LocaleRecord.FromEntity));
  }
}
