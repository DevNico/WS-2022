using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceUsers;

public class Create : EndpointBase
  .WithRequest<CreateServiceUserRequest>
  .WithActionResult<ServiceUserRecord>
{
  private readonly IServiceUserService _service;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public Create(IServiceUserService service,
    IServiceManagerAuthorizationService authorizationService)
  {
    _service = service;
    _authorizationService = authorizationService;
  }

  [HttpPost]
  [SwaggerOperation(
    Summary = "Create a new service user",
    OperationId = "ServiceUser.Create",
    Tags = new[] { "ServiceUser" }
  )]
  [SwaggerResponse(200, "The service user was created", typeof(ServiceUserRecord))]
  [SwaggerResponse(404, "One of the dependencies was not found")]
  public override async Task<ActionResult<ServiceUserRecord>> HandleAsync(
    CreateServiceUserRequest request,
    CancellationToken cancellationToken = new())
  {
    if (!await _authorizationService.EvaluateOrganisationAuthorizationServiceId(User,
          request.ServiceId, ServiceUserOperations.ServiceUser_Create, cancellationToken))
    {
      return Unauthorized();
    }

    var user = await _service.Create(request.ServiceId, request.ServiceRoleId,
      request.OrganisationUserId, cancellationToken);
    return this.ToActionResult(user.MapValue(ServiceUserRecord.FromEntity));
  }
}
