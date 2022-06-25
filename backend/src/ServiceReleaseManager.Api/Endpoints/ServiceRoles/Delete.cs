using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceRoles;

public class Delete : EndpointBase
  .WithRequest<DeleteServiceRole>
  .WithoutResult
{
  private readonly IServiceRoleService _service;

  public Delete(IServiceRoleService service)
  {
    _service = service;
  }

  [HttpDelete(DeleteServiceRole.Route)]
  [SwaggerOperation(
    Summary = "Delete a service role",
    OperationId = "ServiceRole.Delete",
    Tags = new[] { "ServiceRole" }
  )]
  [SwaggerResponse(200, "The service role was deactivated")]
  [SwaggerResponse(404, "The service role was not found")]
  public override async Task<ActionResult> HandleAsync(DeleteServiceRole request,
    CancellationToken cancellationToken = new())
  {
    var result = await _service.Deactivate(request.ServiceRoleId, cancellationToken);
    return this.ToActionResult(result);
  }
}
