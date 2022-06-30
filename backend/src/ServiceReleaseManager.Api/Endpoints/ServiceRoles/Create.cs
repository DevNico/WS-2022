using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceRoles;

public class
  Create : EndpointBase.WithRequest<CreateServiceRole>.WithActionResult<ServiceRoleRecord>
{
  private readonly IServiceManagerAuthorizationService _authorizationService;
  private readonly IServiceRoleService _service;

  public Create(
    IServiceRoleService service,
    IServiceManagerAuthorizationService authorizationService
  )
  {
    _service = service;
    _authorizationService = authorizationService;
  }

  [HttpPost]
  [SwaggerOperation(
    Summary = "Creates a new service role",
    OperationId = "ServiceRole.Create",
    Tags = new[] { "ServiceRole" }
  )]
  [SwaggerResponse(200, "The service role was created", typeof(ServiceRoleRecord))]
  [SwaggerResponse(408, "A role with the same name already exists")]
  public override async Task<ActionResult<ServiceRoleRecord>> HandleAsync(
    CreateServiceRole request,
    CancellationToken cancellationToken = new()
  )
  {
    if (!await _authorizationService.EvaluateOrganisationAuthorization(
          User,
          request.OrganisationId,
          ServiceRoleOperations.ServiceRole_Create,
          cancellationToken
        ))
    {
      return Unauthorized();
    }

    var found = await _service.GetByName(request.Name, cancellationToken);
    if (found.IsSuccess && found.Value.Exists(r => r.OrganisationId == request.OrganisationId))
    {
      return Conflict(new ErrorResponse("A role with the same name already exists"));
    }

    var role = new ServiceRole(
      request.OrganisationId,
      request.Name,
      request.ReleaseCreate,
      request.ReleaseApprove,
      request.ReleasePublish,
      request.ReleaseMetadataEdit,
      request.ReleaseLocalizedMetadataEdit
    );
    var result = await _service.Create(request.OrganisationId, role, cancellationToken);
    return this.ToActionResult(result.MapValue(ServiceRoleRecord.FromEntity));
  }
}
