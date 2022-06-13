using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class List : EndpointBaseAsync.WithRequest<ListOrganisationUserRequest>.WithActionResult<
  List<OrganisationUserRecord>>
{
  public List(IOrganisationUserService organisationUserService)
  {
    _organisationUserService = organisationUserService;
  }

  private readonly IOrganisationUserService _organisationUserService;

  [HttpGet(ListOrganisationUserRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets a list of all OrganisationUsers",
    OperationId = "OrganisationUser.List",
    Tags = new[] { "OrganisationUser" })
  ]
  public override async Task<ActionResult<List<OrganisationUserRecord>>> HandleAsync(
    [FromRoute] ListOrganisationUserRequest request,
    CancellationToken cancellationToken = new())
  {
    var result = await
      _organisationUserService.ListByOrganisationRouteName(request.OrganisationRouteName,
        cancellationToken);

    return this.ToActionResult(result.MapValue(users =>
      users.ConvertAll(OrganisationUserRecord.FromEntity)));
  }
}
