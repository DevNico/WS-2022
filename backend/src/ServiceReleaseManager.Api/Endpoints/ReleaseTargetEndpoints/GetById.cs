using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Core.ReleaseAggregate;
// using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseTargetEndpoints;

public class GetById : EndpointBaseAsync
  .WithRequest<GetReleaseTargetByIdRequest>
  .WithActionResult<ReleaseTargetRecord>
{
  private readonly IRepository<ReleaseTarget> _repository;

  public GetById(IRepository<ReleaseTarget> repository)
  {
    _repository = repository;
  }

  [HttpGet(GetReleaseTargetByIdRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets a single Release Target",
    Description = "Gets a single Release Target by it's id",
    OperationId = "ReleaseTarget.GetById",
    Tags = new[] { "Release Target Endpoints" })
  ]
  public override async Task<ActionResult<ReleaseTargetRecord>> HandleAsync(
    [FromRoute] GetReleaseTargetByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    //var spec = new ReleaseTargetByIdSpec(request.ReleaseTargetId);
    //var releaseTarget = await _repository.GetBySpecAsync(spec, cancellationToken);

    //if (releaseTarget == null)
    //{
    //  return NotFound();
    //}

    //var response = ReleaseTargetRecord.FromEntity(releaseTarget);
    //return Ok(response);

    return NoContent();
  }
}
