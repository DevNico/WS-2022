using Ardalis.ApiEndpoints;
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

    var locale = await _localeService.Create(
      new Locale(request.CountryCode, request.LanguageCode,
        request.IsDefault.GetValueOrDefault(false), request.ServiceId), cancellationToken);

    return this.ToActionResult(locale.MapValue(LocaleRecord.FromEntity));
  }
}
