using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Organisation;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoles;

public class Delete : EndpointBase.WithRequest<DeleteOrganisationRoleRequest>.WithoutResult
{
  private readonly IOrganisationRoleService _organisationRoleService;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public Delete(IOrganisationRoleService organisationRoleService, IServiceManagerAuthorizationService authorizationService)
  {
    _organisationRoleService = organisationRoleService;
    _authorizationService = authorizationService;
  }

  [HttpDelete(DeleteOrganisationRoleRequest.Route)]
  [SwaggerOperation(
    Summary = "Deletes a OrganisationRole",
    Description = "Deletes a OrganisationRole",
    OperationId = "OrganisationRoles.Delete",
    Tags = new[] { "OrganisationRole" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteOrganisationRoleRequest request,
    CancellationToken cancellationToken = new())
  {
    if (!await _authorizationService.EvaluateOrganisationAuthorization(User,
      OrganisationRoleOperation.OrganisationRole_Delete, cancellationToken))
    {
      return NotFound();
    }

    var result =
      await _organisationRoleService.Delete(request.OrganisationRoleId, cancellationToken);

    return result.IsSuccess ? NoContent() : this.ToActionResult(result);
  }
}
