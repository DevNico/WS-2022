using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Organisation;
using ServiceReleaseManager.Api.Endpoints.OrganisationRoles;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListOrganisationRoles : EndpointBase.WithRequest<ListOrganisationRolesRequest>.
  WithActionResult<
    List<OrganisationRoleRecord>>
{
  private readonly IServiceManagerAuthorizationService _authorizationService;
  private readonly IOrganisationRoleService _organisationRoleService;

  public ListOrganisationRoles(
    IOrganisationRoleService organisationRoleService,
    IServiceManagerAuthorizationService authorizationService
  )
  {
    _organisationRoleService = organisationRoleService;
    _authorizationService = authorizationService;
  }

  [HttpGet(ListOrganisationRolesRequest.Route)]
  [SwaggerOperation(
      Summary = "Gets a list of all OrganisationRoles",
      OperationId = "OrganisationRoles.List",
      Tags = new[] { "OrganisationRole" }
    )
  ]
  [SwaggerResponse(
    StatusCodes.Status200OK,
    "OrganisationRoles found",
    typeof(List<OrganisationRoleRecord>)
  )]
  public override async Task<ActionResult<List<OrganisationRoleRecord>>> HandleAsync(
    [FromRoute] ListOrganisationRolesRequest request,
    CancellationToken cancellationToken = new()
  )
  {
    if (!await _authorizationService.EvaluateOrganisationAuthorization(
          User,
          request.OrganisationRouteName,
          OrganisationRoleOperation.OrganisationRole_List,
          cancellationToken
        ))
    {
      return Unauthorized();
    }

    var result = await
      _organisationRoleService.ListByOrganisationRouteName(
        request.OrganisationRouteName,
        cancellationToken
      );

    return this.ToActionResult(
      result.MapValue(
        role =>
          role.ConvertAll(OrganisationRoleRecord.FromEntity)
      )
    );
  }
}
