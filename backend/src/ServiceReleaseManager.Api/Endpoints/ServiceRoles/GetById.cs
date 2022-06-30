using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceRoles;

public class
  GetById : EndpointBase.WithRequest<GetServiceRoleById>.WithActionResult<ServiceRoleRecord>
{
  private readonly IServiceManagerAuthorizationService _authorizationService;
  private readonly IServiceRoleService _service;

  public GetById(
    IServiceRoleService service,
    IServiceManagerAuthorizationService authorizationService
  )
  {
    _service = service;
    _authorizationService = authorizationService;
  }

  [HttpGet(GetServiceRoleById.Route)]
  [SwaggerOperation(
    Summary = "Get a service role by its id",
    OperationId = "ServiceRole.GetById",
    Tags = new[] { "ServiceRole" }
  )]
  public override async Task<ActionResult<ServiceRoleRecord>> HandleAsync(
    GetServiceRoleById request,
    CancellationToken cancellationToken = new()
  )
  {
    var role = await _service.GetById(request.ServiceRoleId, cancellationToken);

    if (!role.IsSuccess || !await _authorizationService.EvaluateOrganisationAuthorization(
          User,
          role.Value.OrganisationId,
          ServiceRoleOperations.ServiceRole_Read,
          cancellationToken
        ))
    {
      return Unauthorized();
    }

    var result = await _service.GetById(request.ServiceRoleId, cancellationToken);
    return this.ToActionResult(result.MapValue(ServiceRoleRecord.FromEntity));
  }
}
