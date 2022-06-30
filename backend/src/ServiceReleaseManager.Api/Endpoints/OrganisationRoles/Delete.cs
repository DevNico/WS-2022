using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoles;

public class Delete : EndpointBase.WithRequest<DeleteOrganisationRoleRequest>.WithoutResult
{
  private readonly IOrganisationRoleService _organisationRoleService;

  public Delete(IOrganisationRoleService organisationRoleService)
  {
    _organisationRoleService = organisationRoleService;
  }

  [HttpDelete(DeleteOrganisationRoleRequest.Route)]
  [SwaggerOperation(
      Summary = "Deletes a OrganisationRole",
      Description = "Deletes a OrganisationRole",
      OperationId = "OrganisationRoles.Delete",
      Tags = new[] { "OrganisationRole" }
    )
  ]
  [SwaggerResponse(201, "OrganisationRole deleted")]
  [SwaggerResponse(404, "OrganisationRole not found")]
  [SwaggerResponse(409, "A User exists depending on this role")]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteOrganisationRoleRequest request,
    CancellationToken cancellationToken = new()
  )
  {
    var result =
      await _organisationRoleService.Delete(request.OrganisationRoleId, cancellationToken);

    if (result.Status == ResultStatus.Invalid)
    {
      return Conflict("The role has unresolved dependents");
    }

    return result.IsSuccess ? NoContent() : this.ToActionResult(result);
  }
}
