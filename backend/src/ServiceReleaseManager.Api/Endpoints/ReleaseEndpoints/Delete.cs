using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
// using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseEndpoints;

public class Delete : EndpointBaseAsync.WithRequest<DeleteReleaseReqest>.WithoutResult
{
  private readonly IRepository<Release> _repository;

  public Delete(IRepository<Release> repository)
  {
    _repository = repository;
  }

  [HttpDelete(DeleteReleaseReqest.Route)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Deletes a release",
    Description = "Deletes a release",
    OperationId = "Release.Delete",
    Tags = new[] { "Release Endpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteReleaseReqest request,
    CancellationToken cancellationToken = new())
  {
    //var spec = new ReleaseByIdSpec(request.ReleaseId);
    //var releaseToDelete = await _repository.GetBySpecAsync(spec, cancellationToken);
    //if (releaseToDelete == null)
    //{
    //  return NotFound();
    //}

    //releaseToDelete.Deactivate();
    //await _repository.SaveChangesAsync(cancellationToken);

    // ToDo: Implement / Add Release Specification / Add Release IsActive flag

    return NoContent();
  }
}
