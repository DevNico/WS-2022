using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUsers;

public class Delete : EndpointBase.WithRequest<DeleteOrganisationUserRequest>.WithoutResult
{
  private readonly IOrganisationUserService _organisationUserService;

  public Delete(IOrganisationUserService organisationUserService)
  {
    _organisationUserService = organisationUserService;
  }

  [HttpDelete(DeleteOrganisationUserRequest.Route)]
  [SwaggerOperation(
    Summary = "Deletes a OrganisationUser",
    Description = "Deletes a OrganisationUser",
    OperationId = "OrganisationUser.Delete",
    Tags = new[] { "OrganisationUser" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteOrganisationUserRequest request,
    CancellationToken cancellationToken = new())
  {
    var result =
      await _organisationUserService.Delete(request.OrganisationUserId, cancellationToken);

    return result.IsSuccess ? NoContent() : this.ToActionResult(result);
  }
}
