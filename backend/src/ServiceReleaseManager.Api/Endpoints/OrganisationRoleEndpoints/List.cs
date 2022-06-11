using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Routes;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class List : EndpointBaseAsync.WithRequest<ListOrganisationRoleRequest>.WithActionResult<
  List<OrganisationRoleRecord>>
{
  private readonly IRepository<Organisation> _repository;

  public List(IRepository<Organisation> repository)
  {
    _repository = repository;
  }

  [HttpGet(RouteHelper.OrganizationRoles_List)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Gets a list of all OrganisationRoles",
    OperationId = "OrganisationRoles.List",
    Tags = new[] { "OrganisationRolesEndpoints" })
  ]
  public override async Task<ActionResult<List<OrganisationRoleRecord>>> HandleAsync(
    ListOrganisationRoleRequest request, CancellationToken cancellationToken = new())
  {
    if (string.IsNullOrWhiteSpace(request.OrganisationName))
    {
      return BadRequest();
    }

    var orgSpec = new OrganisationByNameSpec(request.OrganisationName);
    var org = await _repository.GetBySpecAsync(orgSpec, cancellationToken);
    if (org == null)
    {
      return BadRequest();
    }


    var response = org.Roles
      .Select(OrganisationRoleRecord.FromEntity)
      .ToList();

    return Ok(response);
  }
}
