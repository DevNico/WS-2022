using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class Delete : EndpointBaseAsync.WithRequest<DeleteLocaleRequest>.WithoutResult
{
  private readonly ILocaleService _localeService;

  public Delete(ILocaleService localeService)
  {
    _localeService = localeService;
  }

  [Authorize]
  [HttpDelete(DeleteLocaleRequest.Route)]
  [SwaggerOperation(
    Summary = "Delete a locale",
    Description = "Deletes a locale from the database",
    OperationId = "Locale.Delete",
    Tags = new[] { "Locale" }
  )]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteLocaleRequest request,
    CancellationToken cancellationToken = new())
  {
    var result = await _localeService.Delete(request.LocaleId, cancellationToken);

    return this.ToActionResult(result);
  }
}
