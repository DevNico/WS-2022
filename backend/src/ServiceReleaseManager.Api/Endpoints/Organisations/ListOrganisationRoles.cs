using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Endpoints.OrganisationRoles;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListOrganisationRoles : EndpointBase.WithRequest<ListOrganisationRolesRequest>.
  WithActionResult<
    List<OrganisationRoleRecord>>
{
  private readonly IOrganisationRoleService _organisationRoleService;

  public ListOrganisationRoles(IOrganisationRoleService organisationRoleService)
  {
    _organisationRoleService = organisationRoleService;
  }

  [HttpGet(ListOrganisationRolesRequest.Route)]
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
