using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class
  GetServiceByRouteName : EndpointBase.WithRequest<GetServiceByRouteNameRequest>.WithActionResult<
    ServiceRecord>
{
  private readonly IServiceService _service;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public GetServiceByRouteName(IServiceService service,
    IServiceManagerAuthorizationService authorizationService)
  {
    _service = service;
    _authorizationService = authorizationService;
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

    if (!service.IsSuccess || !await _authorizationService.EvaluateOrganisationAuthorization(User,
          service.Value.OrganisationId,
          ServiceOperations.Service_Read, cancellationToken))
    {
      return NotFound();
    }

    return this.ToActionResult(service.MapValue(ServiceRecord.FromEntity));
  }
}
