using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Locales;

public class Delete : EndpointBase.WithRequest<DeleteLocaleRequest>.WithoutResult
{
  private readonly ILocaleService _localeService;

  public Delete(ILocaleService localeService)
  {
    _localeService = localeService;
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
    var result = await _localeService.Delete(request.LocaleId, cancellationToken);

    return this.ToActionResult(result);
  }
}
