using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class ListOrganisationRoles : EndpointBaseAsync.WithRequest<ListOrganisationRolesRequest>.WithActionResult<
    List<OrganisationRoleRecord>>
{
  public ListOrganisationRoles(IOrganisationRoleService organisationRoleService)
  {
    _organisationRoleService = organisationRoleService;
  }

  private readonly IOrganisationRoleService _organisationRoleService;

  [HttpGet(ListOrganisationRolesRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets a list of all OrganisationRoles",
    OperationId = "OrganisationRoles.List",
    Tags = new[] { "OrganisationRole" })
  ]
  public override async Task<ActionResult<List<OrganisationRoleRecord>>> HandleAsync(
    [FromRoute] ListOrganisationRolesRequest request,
    CancellationToken cancellationToken = new())
  {
    var result = await
      _organisationRoleService.ListByOrganisationRouteName(request.OrganisationRouteName,
        cancellationToken);

    return this.ToActionResult(result.MapValue(role =>
      role.ConvertAll(OrganisationRoleRecord.FromEntity)));
  }
}
