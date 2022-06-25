using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceRoles;

public class GetById : EndpointBase
  .WithRequest<GetServiceRoleById>
  .WithActionResult<ServiceRoleRecord>
{
  private readonly IServiceRoleService _service;

  public GetById(IServiceRoleService service)
  {
    _service = service;
  }

  [HttpGet(GetServiceRoleById.Route)]
  [SwaggerOperation(
    Summary = "Get a service role by its id",
    OperationId = "ServiceRole.GetById",
    Tags = new[] { "ServiceRole" }
  )]
  public override async Task<ActionResult<ServiceRoleRecord>> HandleAsync(
    GetServiceRoleById request,
    CancellationToken cancellationToken = new())
  {
    var result = await _service.GetById(request.ServiceRoleId, cancellationToken);
    return this.ToActionResult(result.MapValue(ServiceRoleRecord.FromEntity));
  }
}
