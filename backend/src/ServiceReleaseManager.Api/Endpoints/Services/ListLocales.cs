using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Endpoints.Locales;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class ListLocales : EndpointBase.WithRequest<ListLocalesByServiceId>.WithActionResult<
  List<LocaleRecord>>
{
  private readonly ILocaleService _localeService;

  public ListLocales(ILocaleService localeService)
  {
    _localeService = localeService;
  }

  [HttpGet(ListLocalesByServiceId.Route)]
  [SwaggerOperation(
    Summary = "List all locales",
    OperationId = "Locales.List",
    Tags = new[] { "Service" }
  )]
  [SwaggerResponse(200, "Locales found", typeof(List<LocaleRecord>))]
  [SwaggerResponse(404, "The service was not found")]
  public override async Task<ActionResult<List<LocaleRecord>>> HandleAsync(
    [FromRoute] ListLocalesByServiceId request,
    CancellationToken cancellationToken = new())
  {
    var locales = await _localeService.ListByServiceId(request.ServiceId, cancellationToken);
    return this.ToActionResult(locales.MapValue(l => l.ConvertAll(LocaleRecord.FromEntity)));
  }
}
