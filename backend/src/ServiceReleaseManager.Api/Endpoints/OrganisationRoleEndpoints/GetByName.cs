using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class GetByName : EndpointBaseAsync.WithRequest<GetOrganisationRoleByNameRequest>.WithActionResult<
  OrganisationRoleRecord>
{
  private readonly IRepository<OrganisationRole> _repository;

  public GetByName(IRepository<OrganisationRole> repository)
  {
    _repository = repository;
  }

  [HttpGet(GetOrganisationRoleByNameRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets a single OrganisationRole",
    Description = "Gets a single OrganisationRoles by Name",
    OperationId = "Organisations.GetByName",
    Tags = new[] { "OrganisationRolesEndpoints" })
  ]
  public override async Task<ActionResult<OrganisationRoleRecord>> HandleAsync(
    [FromRoute] GetOrganisationRoleByNameRequest request,
    CancellationToken cancellationToken = new())
  {
    if(request?.Name == null)
    {
      return BadRequest();
    }

    var spec = new OrganisationRoleByNameSpec(request.Name);
    var organisationRole = await _repository.GetBySpecAsync(spec, cancellationToken);

    if (organisationRole == null)
    {
      return NotFound();
    }

    var response = OrganisationRoleRecord.FromEntity(organisationRole);
    return Ok(response);
  }
}
