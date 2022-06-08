using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class List : EndpointBaseAsync.WithoutRequest.WithActionResult<List<OrganisationRoleRecord>>
{
  private readonly IRepository<OrganisationRole> _repository;

  public List(IRepository<OrganisationRole> repository)
  {
    _repository = repository;
  }

  [HttpGet("/organisationroles")]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Gets a list of all OrganisationRoles",
    OperationId = "OrganisationRoles.List",
    Tags = new[] { "OrganisationRolesEndpoints" })
  ]
  public override async Task<ActionResult<List<OrganisationRoleRecord>>> HandleAsync(
    CancellationToken cancellationToken = new())
  {
    var organisationRoles = await _repository.ListAsync(cancellationToken);

    var response = organisationRoles
      .Select(OrganisationRoleRecord.FromEntity)
      .ToList();

    return Ok(response);
  }
}
