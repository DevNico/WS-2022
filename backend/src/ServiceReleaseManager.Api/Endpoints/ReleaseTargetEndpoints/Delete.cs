using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
// using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseTargetEndpoints;

public class Delete : EndpointBaseAsync
  .WithRequest<DeleteReleaseTargetRequest>
  .WithoutResult
{
  private readonly IRepository<ReleaseTarget> _repository;

  public Delete(IRepository<ReleaseTarget> repository)
  {
    _repository = repository;
  }

  [HttpDelete(DeleteReleaseTargetRequest.Route)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Deletes a Release Target",
    Description = "Deletes a Release Target",
    OperationId = "ReleaseTarget.Delete",
    Tags = new[] { "Release Target Endpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteReleaseTargetRequest request,
    CancellationToken cancellationToken = new())
  {
    //var spec = new ReleaseTargetByIdSpec(request.ReleaseTargetId);
    //var releaseTargetToDelete = await _repository.GetBySpecAsync(spec, cancellationToken);
    //if (releaseTargetToDelete == null)
    //{
    //  return NotFound();
    //}

    //releaseTargetToDelete.Deactivate();
    //await _repository.SaveChangesAsync(cancellationToken);

    return NoContent();
  }
}
