using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class
  GetById : EndpointBase.WithRequest<GetServiceByIdRequest>.WithActionResult<ServiceRecord>
{
  private readonly IServiceManagerAuthorizationService _authorizationService;
  private readonly IServiceService _service;

  public GetById(IServiceService service, IServiceManagerAuthorizationService authorizationService)
  {
    _service = service;
    _authorizationService = authorizationService;
  }

  [HttpGet(GetServiceByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Get a service",
    Description = "Get a service by its id",
    OperationId = "Service.GetById",
    Tags = new[] { "Service" }
  )]
  [SwaggerResponse(200, "The service was found", typeof(ServiceRecord))]
  [SwaggerResponse(400, "The service was not found")]
  public override async Task<ActionResult<ServiceRecord>> HandleAsync(
    [FromRoute] GetServiceByIdRequest request,
    CancellationToken cancellationToken = new()
  )
  {
    var service = await _service.GetById(request.ServiceId, cancellationToken);

    if (!service.IsSuccess || !await _authorizationService.EvaluateOrganisationAuthorization(
          User,
          service.Value.OrganisationId,
          ServiceOperations.Service_Read,
          cancellationToken
        ))
    {
      return Unauthorized();
    }

    return this.ToActionResult(service.MapValue(ServiceRecord.FromEntity));
  }
}
