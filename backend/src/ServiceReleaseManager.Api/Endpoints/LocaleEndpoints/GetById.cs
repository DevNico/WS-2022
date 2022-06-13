using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class GetById : EndpointBaseAsync.WithRequest<GetLocaleByIdRequest>.WithActionResult<
  LocaleRecord>
{
  private readonly ILocaleService _localeService;

  public GetById(ILocaleService localeService)
  {
    _localeService = localeService;
  }

  [Authorize]
  [HttpGet(GetLocaleByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Get a locale by id",
    Description = "Get a locale by id",
    OperationId = "Locale.GetById",
    Tags = new[] { "Locale" }
  )]
  public override async Task<ActionResult<LocaleRecord>> HandleAsync(
    [FromRoute] GetLocaleByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    var locale = await _localeService.GetById(request.LocaleId, cancellationToken);

    return this.ToActionResult(locale.MapValue(LocaleRecord.FromEntity));
  }
}
