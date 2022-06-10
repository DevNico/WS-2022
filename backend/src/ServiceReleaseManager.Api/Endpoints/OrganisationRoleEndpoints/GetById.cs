using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Routes;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class GetById : EndpointBaseAsync.WithRequest<GetOrganisationRoleByIdRequest>.WithActionResult<
  OrganisationRoleRecord>
{
  private readonly IRepository<Organisation> _repository;

  public GetById(IRepository<Organisation> repository)
  {
    _repository = repository;
  }

  [HttpGet(RouteHelper.OrganizationRoles_GetByName)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets a single OrganisationRole",
    Description = "Gets a single OrganisationRoles by Name",
    OperationId = "Organisations.GetByName",
    Tags = new[] { "OrganisationRolesEndpoints" })
  ]
  public override async Task<ActionResult<OrganisationRoleRecord>> HandleAsync(
    [FromRoute] GetOrganisationRoleByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    if(string.IsNullOrWhiteSpace(request?.OrganisationName))
    {
      return BadRequest();
    }

    var orgSpec = new OrganisationByNameSpec(request.OrganisationName);
    var org = await _repository.GetBySpecAsync(orgSpec, cancellationToken);
    if (org == null)
    {
      return Unauthorized();
    }

    var spec = new OrganisationRoleByIdSpec(request.RoleId);
    var organisationRole = spec.Evaluate(org.Roles).FirstOrDefault();

    if (organisationRole == null)
    {
      return NotFound();
    }

    var response = OrganisationRoleRecord.FromEntity(organisationRole);
    return Ok(response);
  }
}
