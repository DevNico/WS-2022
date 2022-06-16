using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
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

  public ListOrganisationUsers(IOrganisationUserService organisationUserService)
  {
    _organisationUserService = organisationUserService;
  }

  [HttpGet(ListOrganisationUsersRequest.Route)]
  [SwaggerOperation(
    Summary = "Gets a list of all OrganisationUsers",
    OperationId = "OrganisationUser.List",
    Tags = new[] { "OrganisationUser" })
  ]
  public override async Task<ActionResult<List<OrganisationUserRecord>>> HandleAsync(
    [FromRoute] ListOrganisationUsersRequest request,
    CancellationToken cancellationToken = new())
  {
    var result = await
      _organisationUserService.ListByOrganisationRouteName(request.OrganisationRouteName,
        cancellationToken);

    return this.ToActionResult(result.MapValue(users =>
      users.ConvertAll(OrganisationUserRecord.FromEntity)));
  }
}
