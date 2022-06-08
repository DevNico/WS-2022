using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Core.ReleaseAggregate;
// using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseEndpoints;

public class GetById : EndpointBaseAsync
  .WithRequest<GetReleaseByIdRequest>
  .WithActionResult<ReleaseRecord>
{
  private readonly IRepository<Release> _repository;

  public GetById(IRepository<Release> repository)
  {
    _repository = repository;
  }

  [HttpGet(GetReleaseByIdRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets a single Release",
    Description = "Gets a single Release by ReleaseId",
    OperationId = "Releases.GetById",
    Tags = new[] { "Release Endpoints" })
  ]
  public override async Task<ActionResult<ReleaseRecord>> HandleAsync(
    [FromRoute] GetReleaseByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    //var spec = new OrganisationByIdSpec(request.OrganisationId);
    //var organisation = await _repository.GetBySpecAsync(spec, cancellationToken);

    //if (organisation == null)
    //{
    //  return NotFound();
    //}

    //var response = OrganisationRecord.FromEntity(organisation);
    //return Ok(response);

    return NoContent();
  }
}
