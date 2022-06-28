using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceRoles;

public class Delete : EndpointBase
  .WithRequest<DeleteServiceRole>
  .WithoutResult
{
  private readonly IServiceRoleService _service;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public Delete(IServiceRoleService service, IServiceManagerAuthorizationService authorizationService)
  {
    _service = service;
    _authorizationService = authorizationService;
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
    var role = await _service.GetById(request.ServiceRoleId, cancellationToken);

    if (!role.IsSuccess || !await _authorizationService.EvaluateOrganisationAuthorization(User,
      role.Value.OrganisationId, ServiceRoleOperations.ServiceRole_Delete, cancellationToken))
    {
      return Unauthorized();
    }

    var result = await _service.Deactivate(request.ServiceRoleId, cancellationToken);
    return this.ToActionResult(result);
  }
}
