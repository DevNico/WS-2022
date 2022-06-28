using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Locale;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Locales;

public class GetById : EndpointBase.WithRequest<GetLocaleByIdRequest>.WithActionResult<
  LocaleRecord>
{
  private readonly ILocaleService _localeService;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public GetById(ILocaleService localeService, IServiceManagerAuthorizationService authorizationService)
  {
    _localeService = localeService;
    _authorizationService = authorizationService;
  }

  [HttpGet(GetLocaleByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Get a locale by id",
    Description = "Get a locale by id",
    OperationId = "Locale.GetById",
    Tags = new[] { "Locale" }
  )]
  [SwaggerResponse(200, "Locale found", typeof(LocaleRecord))]
  [SwaggerResponse(404, "Locale not found")]
  public override async Task<ActionResult<LocaleRecord>> HandleAsync(
    [FromRoute] GetLocaleByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    var locale = await _localeService.GetById(request.LocaleId, cancellationToken);

    if (!locale.IsSuccess || !await _authorizationService.EvaluateServiceAuthorization(User, locale.Value.ServiceId,
      LocaleOperations.Locale_Read, cancellationToken))
    {
      return NotFound();
    }

    return this.ToActionResult(locale.MapValue(LocaleRecord.FromEntity));
  }
}
