using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Releases;

public class GetById : EndpointBaseAsync.WithRequest<GetReleaseByIdRequest>.WithActionResult<
  ReleaseRecord>
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
    var spec = new ReleaseByIdSpec(request.ReleaseId);
    var release = await _repository.GetBySpecAsync(spec, cancellationToken);

    if (release == null)
    {
      return NotFound();
    }

    var response = ReleaseRecord.FromEntity(release);
    return Ok(response);
  }
}
