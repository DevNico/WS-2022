using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Api.Endpoints.ServiceRoles;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListServiceRoles : EndpointBase
  .WithRequest<ListServiceRolesRequest>
  .WithActionResult<List<ServiceRoleRecord>>
{
  private readonly IServiceRoleService _service;
  private readonly IOrganisationService _organisationService;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public ListServiceRoles(IServiceRoleService service, IOrganisationService organisationService,
    IServiceManagerAuthorizationService authorizationService)
  {
    _service = service;
    _organisationService = organisationService;
    _authorizationService = authorizationService;
  }

  [HttpGet(ListServiceRolesRequest.Route)]
  [SwaggerOperation(
    Summary = "Get all service roles by their id",
    OperationId = "Organisation.ListServiceRoles",
    Tags = new[] { "Organisation" }
  )]
  [SwaggerResponse(200, "The operation was successful", typeof(List<ServiceRoleRecord>))]
  [SwaggerResponse(404, "The organisation does not exist")]
  public override async Task<ActionResult<List<ServiceRoleRecord>>> HandleAsync(
    [FromRoute] ListServiceRolesRequest request,
    CancellationToken cancellationToken = new())
  {
    if (!await _authorizationService.EvaluateOrganisationAuthorization(User,
          request.OrganisationRouteName, ServiceRoleOperations.ServiceRole_List, cancellationToken))
    {
      return Unauthorized();
    }

    var organisation =
      await _organisationService.GetByRouteName(request.OrganisationRouteName, cancellationToken);
    if (!organisation.IsSuccess)
    {
      return NotFound();
    }

    var result = await _service.GetByOrganisationId(organisation.Value.Id, cancellationToken);
    return this.ToActionResult(result.MapValue(r => r.ConvertAll(ServiceRoleRecord.FromEntity)));
  }
}
