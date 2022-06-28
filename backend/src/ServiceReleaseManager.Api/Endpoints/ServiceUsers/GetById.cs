using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceUsers;

public class GetById : EndpointBase
  .WithRequest<GetServiceUserById>
  .WithActionResult<ServiceUserRecord>
{
  private readonly IServiceUserService _service;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public GetById(IServiceUserService service, IServiceManagerAuthorizationService authorizationService
    )
  {
    _service = service;
    _authorizationService = authorizationService;
  }

  [HttpGet(GetServiceUserById.Route)]
  [SwaggerOperation(
    Summary = "Get a service user by its id",
    OperationId = "ServiceUser.GetById",
    Tags = new[] { "ServiceUser" }
  )]
  [SwaggerResponse(200, "Service user found", typeof(ServiceUserRecord))]
  [SwaggerResponse(404, "Service user not found", typeof(NotFoundResult))]
  public override async Task<ActionResult<ServiceUserRecord>> HandleAsync(
    [FromRoute] GetServiceUserById request,
    CancellationToken cancellationToken = new())
  {
    var serviceUser = await _service.GetById(request.ServiceUserId, cancellationToken);

    if (!serviceUser.IsSuccess || !await _authorizationService.EvaluateOrganisationAuthorizationServiceId(User,
      serviceUser.Value.ServiceId, ServiceUserOperations.ServiceUser_Read, cancellationToken))
    {
      return Unauthorized();
    }

    var result = await _service.GetById(request.ServiceUserId, cancellationToken);
    return this.ToActionResult(result.MapValue(ServiceUserRecord.FromEntity));
  }
}
