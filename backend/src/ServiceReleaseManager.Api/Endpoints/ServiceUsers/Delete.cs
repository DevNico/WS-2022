using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceUsers;

public class Delete : EndpointBase
  .WithRequest<DeleteServiceUser>
  .WithoutResult
{
  private readonly IServiceUserService _service;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public Delete(IServiceUserService service,
    IServiceManagerAuthorizationService authorizationService)
  {
    _service = service;
    _authorizationService = authorizationService;
  }

  [HttpDelete(DeleteServiceUser.Route)]
  [SwaggerOperation(
    Summary = "Deletes a service user",
    OperationId = "ServiceUser.Delete",
    Tags = new[] { "ServiceUser" }
  )]
  [SwaggerResponse(200, "The user was disabled")]
  [SwaggerResponse(404, "A user with the given id does not exist")]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteServiceUser request,
    CancellationToken cancellationToken = new())
  {
    var serviceUser = await _service.GetById(request.ServiceUserId, cancellationToken);

    if (!serviceUser.IsSuccess ||
        !await _authorizationService.EvaluateOrganisationAuthorizationServiceId(User,
          serviceUser.Value.ServiceId, ServiceUserOperations.ServiceUser_Delete, cancellationToken))
    {
      return Unauthorized();
    }

    var result = await _service.Deactivate(request.ServiceUserId, cancellationToken);
    return this.ToActionResult(result);
  }
}
