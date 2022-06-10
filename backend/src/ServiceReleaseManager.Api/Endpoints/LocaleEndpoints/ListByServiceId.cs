using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Sepcifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class ListByServiceId : EndpointBaseAsync
  .WithRequest<ListLocalesByServiceId>
  .WithActionResult<List<LocaleRecord>>
{
  private readonly IRepository<Service> _repository;

  public ListByServiceId(IRepository<Service> repository)
  {
    _repository = repository;
  }

  [HttpGet(ListLocalesByServiceId.Route)]
  [SwaggerOperation(
    Summary = "List all locales",
    OperationId = "Locales.List",
    Tags = new[] { "LocaleEndpoints" }
  )]
  public override async Task<ActionResult<List<LocaleRecord>>> HandleAsync(
    [FromRoute] ListLocalesByServiceId request,
    CancellationToken cancellationToken = new())
  {
    var spec = new ServiceByIdSpec(request.ServiceId);
    var service = await _repository.GetBySpecAsync(spec, cancellationToken);

    if (service == null)
    {
      return NotFound();
    }

    var response = service.Locales.ConvertAll(LocaleRecord.FromEntity);
    return Ok(response);
  }
}
