using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
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

  [Authorize]
  [HttpGet(ListLocalesByServiceId.Route)]
  [SwaggerOperation(
    Summary = "List all locales",
    OperationId = "Locales.List",
    Tags = new[] { "LocaleEndpoints" }
  )]
  [SwaggerResponse(200, "Locales found", typeof(List<LocaleRecord>))]
  [SwaggerResponse(404, "The service was not found")]
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
