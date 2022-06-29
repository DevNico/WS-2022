using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Locale;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Locales;

public class Delete : EndpointBase.WithRequest<DeleteLocaleRequest>.WithoutResult
{
  private readonly ILocaleService _localeService;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public Delete(ILocaleService localeService,
    IServiceManagerAuthorizationService authorizationService)
  {
    _localeService = localeService;
    _authorizationService = authorizationService;
  }

  [HttpDelete(DeleteLocaleRequest.Route)]
  [SwaggerOperation(
    Summary = "Delete a locale",
    Description = "Deletes a locale from the database",
    OperationId = "Locale.Delete",
    Tags = new[] { "Locale" }
  )]
  [SwaggerResponse(201, "Locale deleted")]
  [SwaggerResponse(404, "The locale or service was not found")]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteLocaleRequest request,
    CancellationToken cancellationToken = new())
  {
    var locale = await _localeService.GetById(request.LocaleId, cancellationToken);

    if (!locale.IsSuccess || !await _authorizationService.EvaluateServiceAuthorization(User,
          locale.Value.ServiceId,
          LocaleOperations.Locale_Delete, cancellationToken))
    {
      return NotFound();
    }

    var result = await _localeService.Delete(request.LocaleId, cancellationToken);

    return this.ToActionResult(result);
  }
}
