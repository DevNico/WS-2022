using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoles;

public class Create : EndpointBase.WithRequest<CreateOrganisationRoleRequest>.WithActionResult<
  OrganisationRoleRecord>
{
  private readonly IOrganisationRoleService _organisationRoleService;

  public Create(IOrganisationRoleService organisationRoleService)
  {
    _organisationRoleService = organisationRoleService;
  }

  [HttpPost]
  [SwaggerOperation(
    Summary = "Creates a new OrganisationRole",
    Description = "Creates a new OrganisationRole",
    OperationId = "OrganisationRole.Create",
    Tags = new[] { "OrganisationRole" })
  ]
  public override async Task<ActionResult<OrganisationRoleRecord>> HandleAsync(
    [FromRoute] CreateOrganisationRoleRequest request,
    CancellationToken cancellationToken = new())
  {
    var newRole = new OrganisationRole(request.OrganisationId, request.Name, request.ServiceWrite,
      request.ServiceDelete, request.UserRead, request.UserWrite, request.UserDelete);

    var result = await _organisationRoleService.Create(newRole, cancellationToken);

    return this.ToActionResult(result.MapValue(OrganisationRoleRecord.FromEntity));
  }
}
