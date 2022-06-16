using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Locales;

public class Create : EndpointBase.WithRequest<CreateLocaleRequest>.WithActionResult<
  LocaleRecord>
{
  private readonly ILocaleService _localeService;

  public Create(ILocaleService localeService)
  {
    _localeService = localeService;
  }

  [HttpPost]
  [SwaggerOperation(
    Summary = "Create a new locale",
    Description = "Create a new locale",
    OperationId = "Locale.Create",
    Tags = new[] { "Locale" }
  )]
  [SwaggerResponse(200, "The locale was created", typeof(LocaleRecord))]
  [SwaggerResponse(400, "The request was invalid")]
  [SwaggerResponse(404, "The service was not found")]
  [SwaggerResponse(409, "The locale already exists")]
  public override async Task<ActionResult<LocaleRecord>> HandleAsync(CreateLocaleRequest request,
    CancellationToken cancellationToken = new())
  {
    if (request.LanguageCode == null || request.CountryCode == null)
    {
      return BadRequest();
    }

    var locale = new Locale(request.CountryCode, request.LanguageCode,
      request.IsDefault.GetValueOrDefault(false), request.ServiceId);

    var createResult = await _localeService.Create(locale, cancellationToken)
      .MapValue(LocaleRecord.FromEntity);

    return createResult.IsError() ? Conflict() : this.ToActionResult(createResult);
  }
}
