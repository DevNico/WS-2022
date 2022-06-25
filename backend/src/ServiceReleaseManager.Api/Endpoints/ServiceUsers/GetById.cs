using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceUsers;

public class GetById : EndpointBase
  .WithRequest<GetServiceUserById>
  .WithActionResult<ServiceUserRecord>
{
  private readonly IServiceUserService _service;

  public GetById(IServiceUserService service)
  {
    _service = service;
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
    var result = await _service.GetById(request.ServiceUserId, cancellationToken);
    return this.ToActionResult(result.MapValue(ServiceUserRecord.FromEntity));
  }
}
