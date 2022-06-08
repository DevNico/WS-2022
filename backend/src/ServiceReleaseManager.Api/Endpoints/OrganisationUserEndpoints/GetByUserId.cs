using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class GetByUserId : EndpointBaseAsync.WithRequest<GetOrganisationUserByUserIdRequest>.WithActionResult<
  OrganisationUserRecord>
{
  private readonly IRepository<OrganisationUser> _repository;

  public GetByUserId(IRepository<OrganisationUser> repository)
  {
    _repository = repository;
  }

  [HttpGet(GetOrganisationUserByUserIdRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets a single OrganisationUser",
    Description = "Gets a single OrganisationUser by UserId",
    OperationId = "Organisations.GetByUserId",
    Tags = new[] { "OrganisationUserEndpoints" })
  ]
  public override async Task<ActionResult<OrganisationUserRecord>> HandleAsync(
    [FromRoute] GetOrganisationUserByUserIdRequest request,
    CancellationToken cancellationToken = new())
  {
    if(request?.UserId == null)
    {
      return BadRequest();
    }

    var spec = new OrganisationUserByUserIdSpec(request.UserId);
    var organisation = await _repository.GetBySpecAsync(spec, cancellationToken);

    if (organisation == null)
    {
      return NotFound();
    }

    var response = OrganisationUserRecord.FromEntity(organisation);
    return Ok(response);
  }
}
