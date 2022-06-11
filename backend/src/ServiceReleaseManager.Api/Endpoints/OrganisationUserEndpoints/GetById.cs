using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Routes;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class GetById : EndpointBaseAsync.WithRequest<GetOrganisationUserByIdRequest>.
  WithActionResult<
    OrganisationUserRecord>
{
  private readonly IRepository<Organisation> _repository;

  public GetById(IRepository<Organisation> repository)
  {
    _repository = repository;
  }

  [HttpGet(RouteHelper.OrganizationUsers_GetById)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets a single OrganisationUser",
    Description = "Gets a single OrganisationUser by UserId",
    OperationId = "Organisations.GetByUserId",
    Tags = new[] { "OrganisationUserEndpoints" })
  ]
  public override async Task<ActionResult<OrganisationUserRecord>> HandleAsync(
    [FromRoute] GetOrganisationUserByIdRequest request,
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
      return Unauthorized();
    }

    var spec = new OrganisationUserByIdSpec(request.OrgUserId);
    var user = spec.Evaluate(org.Users).FirstOrDefault();

    if (user == null)
    {
      return NotFound();
    }

    var response = OrganisationUserRecord.FromEntity(user);
    return Ok(response);
  }
}
