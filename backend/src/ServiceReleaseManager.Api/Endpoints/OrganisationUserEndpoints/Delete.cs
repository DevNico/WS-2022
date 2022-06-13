using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class Delete : EndpointBaseAsync.WithRequest<DeleteOrganisationUserRequest>.WithoutResult
{
  public Delete(IOrganisationUserService organisationUserService)
  {
    _organisationUserService = organisationUserService;
  }

  private readonly IOrganisationUserService _organisationUserService;

  [HttpDelete(DeleteOrganisationUserRequest.Route)]
  [Authorize]
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
