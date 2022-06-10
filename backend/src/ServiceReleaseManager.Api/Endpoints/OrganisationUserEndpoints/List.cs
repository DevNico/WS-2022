using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class List : EndpointBaseAsync.WithRequest<ListOrganisationUserRequest>.WithActionResult<List<OrganisationUserRecord>>
{
  private readonly IRepository<Organisation> _repository;

  public List(IRepository<Organisation> repository)
  {
    _repository = repository;
  }

  [HttpGet("/organisations/{OrganisationId:int}/users")]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Gets a list of all OrganisationUsers",
    OperationId = "OrganisationUser.List",
    Tags = new[] { "OrganisationUserEndpoints" })
  ]
  public override async Task<ActionResult<List<OrganisationUserRecord>>> HandleAsync(ListOrganisationUserRequest request,
    CancellationToken cancellationToken = new())
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

    var spec = new ActiveOrganisationUsersSearchSpec();
    var activeOrganisations = spec.Evaluate(org.Users);

    var response = activeOrganisations
      .Select(OrganisationUserRecord.FromEntity)
      .ToList();

    return Ok(response);
  }
}
