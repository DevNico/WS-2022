using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Organisation;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUsers;

public class Delete : EndpointBase.WithRequest<DeleteOrganisationUserRequest>.WithoutResult
{
  private readonly IOrganisationUserService _organisationUserService;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public Delete(IOrganisationUserService organisationUserService, IServiceManagerAuthorizationService authorizationService)
  {
    _organisationUserService = organisationUserService;
    _authorizationService = authorizationService;
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
    if(!await _authorizationService.EvaluateOrganisationAuthorization(User,
      OrganisationUserOperations.OrganisationUser_Delete, cancellationToken))
    {
      return NotFound();
    }

    var result =
      await _organisationUserService.Delete(request.OrganisationUserId, cancellationToken);

    return result.IsSuccess ? NoContent() : this.ToActionResult(result);
  }
}
