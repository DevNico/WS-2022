using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceUsers;

public class Delete : EndpointBase
  .WithRequest<DeleteServiceUser>
  .WithoutResult
{
  private readonly IServiceUserService _service;

  public Delete(IServiceUserService service)
  {
    _service = service;
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
    var result = await _service.Deactivate(request.ServiceUserId, cancellationToken);
    return this.ToActionResult(result);
  }
}
