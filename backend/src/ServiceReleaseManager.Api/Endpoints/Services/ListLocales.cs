using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Locale;
using ServiceReleaseManager.Api.Endpoints.Locales;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class ListLocales : EndpointBase.WithRequest<ListLocalesByServiceRouteName>.WithActionResult<
  List<LocaleRecord>>
{
  private readonly IServiceManagerAuthorizationService _authorizationService;
  private readonly ILocaleService _localeService;

  public ListLocales(
    ILocaleService localeService,
    IServiceManagerAuthorizationService authorizationService
  )
  {
    _localeService = localeService;
    _authorizationService = authorizationService;
  }

  [HttpGet(ListLocalesByServiceRouteName.Route)]
  [SwaggerOperation(
    Summary = "List all locales",
    OperationId = "Locales.List",
    Tags = new[] { "Service" }
  )]
  [SwaggerResponse(200, "Locales found", typeof(List<LocaleRecord>))]
  [SwaggerResponse(404, "The service was not found")]
  public override async Task<ActionResult<List<LocaleRecord>>> HandleAsync(
    [FromRoute] ListLocalesByServiceRouteName request,
    CancellationToken cancellationToken = new()
  )
  {
    if (!await _authorizationService.EvaluateServiceAuthorization(
          User,
          request.ServiceRouteName,
          LocaleOperations.Locale_List,
          cancellationToken
        ))
    {
      return Unauthorized();
    }

    var locales =
      await _localeService.ListByServiceRouteName(request.ServiceRouteName, cancellationToken);
    return this.ToActionResult(locales.MapValue(l => l.ConvertAll(LocaleRecord.FromEntity)));
  }
}
