using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Organisation;
using ServiceReleaseManager.Api.Endpoints.OrganisationUsers;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListOrganisationUsers : EndpointBase.WithRequest<ListOrganisationUsersRequest>.
  WithActionResult<
    List<OrganisationUserRecord>>
{
  private readonly IOrganisationUserService _organisationUserService;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public ListOrganisationUsers(IOrganisationUserService organisationUserService, IServiceManagerAuthorizationService authorizationService)
  {
    _organisationUserService = organisationUserService;
    _authorizationService = authorizationService;
  }

  [HttpGet(ListOrganisationUsersRequest.Route)]
  [SwaggerOperation(
    Summary = "Gets a list of all OrganisationUsers",
    OperationId = "OrganisationUser.List",
    Tags = new[] { "OrganisationUser" })
  ]
  [SwaggerResponse(StatusCodes.Status200OK, "OrganisationUsers found",
    typeof(List<OrganisationUserRecord>))]
  public override async Task<ActionResult<List<OrganisationUserRecord>>> HandleAsync(
    [FromRoute] ListOrganisationUsersRequest request,
    CancellationToken cancellationToken = new())
  {

    if (!await _authorizationService.EvaluateOrganisationAuthorization(User, request.OrganisationRouteName, OrganisationUserOperations.OrganisationUser_List, cancellationToken))
    {
      return Unauthorized();
    }

    var result = await
      _organisationUserService.ListByOrganisationRouteName(request.OrganisationRouteName,
        cancellationToken);

    return this.ToActionResult(result.MapValue(users =>
      users.ConvertAll(OrganisationUserRecord.FromEntity)));
  }
}
