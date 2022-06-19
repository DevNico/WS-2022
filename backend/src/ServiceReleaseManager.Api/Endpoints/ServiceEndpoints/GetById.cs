using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceEndpoints;

public class GetById : AuthorizedEndpointBase
  .WithRequest<GetServiceByIdRequest>
  .WithActionResult<ServiceRecord>
{
  private readonly IRepository<Service> _repository;

  public GetById(IRepository<Service> repository)
  {
    _repository = repository;
  }

  [HttpGet(GetServiceByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Get a service",
    Description = "Get a service by its id",
    OperationId = "Service.GetById",
    Tags = new[] { "ServiceEndpoints" }
  )]
  [SwaggerResponse(200, "The service was found", typeof(ServiceRecord))]
  [SwaggerResponse(404, "The service was not found")]
  public override async Task<ActionResult<ServiceRecord>> HandleAsync(
    [FromRoute] GetServiceByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    var spec = new ServiceByIdSpec(request.ServiceId);
    var service = await _repository.GetBySpecAsync(spec, cancellationToken);
    if (service == null)
    {
      return NotFound();
    }

    var response = ServiceRecord.FromEntity(service);
    return Ok(response);
  }
}
