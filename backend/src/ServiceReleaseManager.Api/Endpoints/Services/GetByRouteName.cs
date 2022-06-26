using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class
  GetServiceByRouteName : EndpointBase.WithRequest<GetServiceByRouteNameRequest>.WithActionResult<
    ServiceRecord>
{
  private readonly IServiceService _service;

  public GetServiceByRouteName(IServiceService service)
  {
    _service = service;
  }

  [HttpGet(GetServiceByRouteNameRequest.Route)]
  [SwaggerOperation(
    Summary = "Get a service",
    Description = "Get a service by its route name",
    OperationId = "Service.GetByRouteName",
    Tags = new[] { "Service" }
  )]
  [SwaggerResponse(200, "The service was found", typeof(ServiceRecord))]
  [SwaggerResponse(404, "The service was not found")]
  public override async Task<ActionResult<ServiceRecord>> HandleAsync(
    [FromRoute] GetServiceByRouteNameRequest request,
    CancellationToken cancellationToken = new())
  {
    var service =
      await _service.GetByRouteName(
        request.ServiceRouteName, cancellationToken);
    return this.ToActionResult(service.MapValue(ServiceRecord.FromEntity));
  }
}
