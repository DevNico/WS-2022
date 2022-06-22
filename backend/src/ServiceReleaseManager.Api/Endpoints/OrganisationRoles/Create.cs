using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Organisation;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoles;

public class Create : EndpointBase.WithRequest<CreateOrganisationRoleRequest>.WithActionResult<
  OrganisationRoleRecord>
{
  private readonly IOrganisationRoleService _organisationRoleService;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public Create(IOrganisationRoleService organisationRoleService, IServiceManagerAuthorizationService authorizationService)
  {
    _organisationRoleService = organisationRoleService;
    _authorizationService = authorizationService;
  }

  [HttpPost]
  [SwaggerOperation(
    Summary = "Creates a new OrganisationRole",
    Description = "Creates a new OrganisationRole",
    OperationId = "OrganisationRole.Create",
    Tags = new[] { "OrganisationRole" })
  ]
  public override async Task<ActionResult<OrganisationRoleRecord>> HandleAsync(
    [FromBody] CreateOrganisationRoleRequest request,
    CancellationToken cancellationToken = new())
  {
    if (!await _authorizationService.EvaluateOrganisationAuthorization(User,
      OrganisationRoleOperation.OrganisationRole_Create, cancellationToken))
    {
      return NotFound();
    }

    var newRole = new OrganisationRole(request.OrganisationId, request.Name, request.ServiceWrite,
      request.ServiceDelete, request.UserRead, request.UserWrite, request.UserDelete);

    var result = await _organisationRoleService.Create(newRole, cancellationToken);

    return this.ToActionResult(result.MapValue(OrganisationRoleRecord.FromEntity));
  }
}
