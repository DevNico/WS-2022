using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceUsers;

public class Create : EndpointBase
  .WithRequest<CreateServiceUserRequest>
  .WithActionResult<ServiceUserRecord>
{
  private readonly IServiceUserService _service;

  public Create(IServiceUserService service)
  {
    _service = service;
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
    var user = await _service.Create(request.ServiceId, request.ServiceRoleId,
      request.OrganisationUserId, cancellationToken);
    return this.ToActionResult(user.MapValue(ServiceUserRecord.FromEntity));
  }
}
