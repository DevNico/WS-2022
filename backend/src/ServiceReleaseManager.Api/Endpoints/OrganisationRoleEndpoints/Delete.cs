using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class Delete : EndpointBaseAsync.WithRequest<DeleteOrganisationRoleRequest>.WithoutResult
{
  public Delete(IOrganisationRoleService organisationRoleService)
  {
    _organisationRoleService = organisationRoleService;
  }

  private readonly IOrganisationRoleService _organisationRoleService;

  [HttpDelete(DeleteOrganisationRoleRequest.Route)]
  [Authorize]
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
    var result =
      await _organisationRoleService.Delete(request.OrganisationRoleId, cancellationToken);

    return result.IsSuccess ? NoContent() : this.ToActionResult(result);
  }
}
