using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class
  GetById : EndpointBase.WithRequest<GetServiceByIdRequest>.WithActionResult<ServiceRecord>
{
  private readonly IServiceService _service;

  public GetById(IServiceService service)
  {
    _service = service;
  }

  [HttpGet(GetServiceByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Get a service",
    Description = "Get a service by its id",
    OperationId = "Service.GetById",
    Tags = new[] { "Service" }
  )]
  [SwaggerResponse(200, "The service was found", typeof(ServiceRecord))]
  [SwaggerResponse(404, "The service was not found")]
  public override async Task<ActionResult<ServiceRecord>> HandleAsync(
    [FromRoute] GetServiceByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    var service = await _service.GetById(request.ServiceId, cancellationToken);
    return this.ToActionResult(service.MapValue(ServiceRecord.FromEntity));
  }
}
