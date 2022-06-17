using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class Delete : EndpointBaseAsync.WithRequest<DeleteLocaleRequest>.WithoutResult
{
  private readonly IRepository<Service> _repository;
  private readonly IRepository<Locale> _localeRepository;

  public Delete(IRepository<Service> repository, IRepository<Locale> localeRepository)
  {
    _repository = repository;
    _localeRepository = localeRepository;
  }

  [Authorize]
  [HttpDelete(DeleteLocaleRequest.Route)]
  [SwaggerOperation(
    Summary = "Delete a locale",
    Description = "Deletes a locale from the database",
    OperationId = "Locale.Delete",
    Tags = new[] { "LocaleEndpoints" }
  )]
  [SwaggerResponse(201, "Locale deleted")]
  [SwaggerResponse(404, "The locale or service was not found")]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteLocaleRequest request,
    CancellationToken cancellationToken = new())
  {
    var localeSpec = new LocaleByIdSpec(request.LocaleId);
    var localeToDelete = await _localeRepository.GetBySpecAsync(localeSpec, cancellationToken);
    if (localeToDelete == null)
    {
      return NotFound();
    }

    var spec = new ServiceByLocaleIdSpec(request.LocaleId);
    var service = await _repository.GetBySpecAsync(spec, cancellationToken);
    if (service == null)
    {
      return NotFound();
    }

    service.Locales.Remove(localeToDelete);
    await _repository.UpdateAsync(service, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    await _localeRepository.DeleteAsync(localeToDelete, cancellationToken);
    await _localeRepository.SaveChangesAsync(cancellationToken);

    return NoContent();
  }
}
