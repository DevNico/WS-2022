using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class Delete : EndpointBaseAsync.WithRequest<DeleteLocaleRequest>.WithoutResult
{
  private readonly IRepository<Locale> _repository;

  public Delete(IRepository<Locale> repository)
  {
    _repository = repository;
  }

  [HttpDelete(DeleteLocaleRequest.Route)]
  [SwaggerOperation(
    Summary = "Delete a locale",
    Description = "Deletes a locale from the database",
    OperationId = "Locale.Delete",
    Tags = new[] { "LocaleEndpoints" }
  )]
  public override async Task<ActionResult> HandleAsync([FromRoute] DeleteLocaleRequest request,
    CancellationToken cancellationToken = new CancellationToken())
  {
    var spec = new LocaleByIdSpec(request.LocaleId);
    var localeToDelete = await _repository.GetBySpecAsync(spec, cancellationToken);
    if (localeToDelete == null)
    {
      return NotFound();
    }

    // TODO: Check for dependencies
    await _repository.DeleteAsync(localeToDelete, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return NoContent();
  }
}
